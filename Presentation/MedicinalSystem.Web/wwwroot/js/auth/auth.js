const token = localStorage.getItem('token');

if (token !== null && !isTokenExpired(token)) {
    // Показываем кнопку выхода
    displayLogoutButton();
} else {
    // Показываем форму регистрации/входа
    displayAuthForm();
}

function displayAuthForm() {
    const authContainer = document.querySelector('.auth-container');
    authContainer.innerHTML = `
        <form id="authForm">
            <input type="text" id="username" placeholder="Username" required />
            <input type="password" id="password" placeholder="Password" required />
            <button type="submit">Login</button>
            <p id="errorMsg" style="display: none; color: red;"></p>
        </form>
        <button id="registerBtn">Register</button>
    `;

    document.getElementById('authForm').addEventListener('submit', async function (e) {
        e.preventDefault();
        const userName = document.getElementById('username').value;
        const password = document.getElementById('password').value;
        const errorMsg = document.getElementById('errorMsg');
        
        // Clear previous error message
        errorMsg.style.display = 'none';

        try {
            const response = await axios.get('/api/auth/login', {
                params:
                {
                    userName: userName,
                    password: password,
                },
            });

            if (typeof(response.ok) !== "undefined" && !response.ok) {
                throw new Error('Invalid credentials');
            }

            const data = await response.data;
            localStorage.setItem('token', data.token);
            localStorage.setItem('role', data.role);
            localStorage.setItem('userName', data.userName);
            localStorage.setItem('fullName', data.fullName);

            const accountLink = document.querySelector('a[href="/Home/Auth"]');

            if (accountLink) {
                accountLink.textContent = data.userName;
            }

            displayLogoutButton();

            // Перезагрузка страницы
            location.reload();

        } catch (error) {
            errorMsg.style.display = 'block';
            errorMsg.textContent = error.message;
        }
    });

    document.getElementById('registerBtn').addEventListener('click', () => {
        window.location.href = '/Home/Reg'; // Перенаправление на страницу регистрации
    });
}

function displayLogoutButton() {
    const authContainer = document.querySelector('.auth-container');
    authContainer.innerHTML = '';
    const userNameHeader = document.createElement('h2');
    userNameHeader.textContent = `Добро пожаловать, ${localStorage.getItem('userName') || 'Гость'}`; 

    const infoContainer = document.createElement('div');
    const fullName = document.createElement('p');
    fullName.textContent = `Полное имя: ${localStorage.getItem('fullName') || 'Не указано'}`;

    const role = document.createElement('p');
    role.textContent = `Роль: ${localStorage.getItem('role') || 'Не указана'}`;

    infoContainer.appendChild(fullName);
    infoContainer.appendChild(role);

    authContainer.appendChild(userNameHeader);
    authContainer.appendChild(infoContainer);

    const logoutButton = document.createElement('button');
    logoutButton.textContent = 'Выйти';

    logoutButton.addEventListener('click', () => {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        localStorage.removeItem('fullName');
        localStorage.removeItem('userName');

        window.location.href = '/Home/Index';
    });

    authContainer.appendChild(logoutButton);
}


// Функции для проверки жизни токена
function base64UrlDecode(base64Url) {
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const decoded = atob(base64);
    return JSON.parse(decoded);
}

function isTokenExpired(token) {
    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
        console.error('Неверный формат токена');
        return true;
    }

    const payload = base64UrlDecode(tokenParts[1]);
    const currentTime = Math.floor(Date.now() / 1000);

    if (payload.exp < currentTime) {
        console.log("Токен истек.");
        return true;
    } else {
        console.log("Токен еще действителен.");
        return false;
    }
}