document.getElementById('registerForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const username = document.getElementById('registerUsername').value;
    const firstName = document.getElementById('registerFirstName').value;
    const lastName = document.getElementById('registerLastName').value;
    const email = document.getElementById('registerEmail').value;
    const password = document.getElementById('registerPassword').value;
    const errorMsg = document.getElementById('registerErrorMsg');
    const newUser = {
        userName: username,
        emai: email,
        password: password,
        firstName: firstName,
        lastName: lastName
    };

    // Clear previous error message
    errorMsg.style.display = 'none';

    try {
        const response = await axios.post('/api/auth/register', newUser, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
            //headers: {
            //    'Content-Type': 'application/json'
            //},
        });

        if (!response.ok) {
            throw new Error('Failed to register');
        }

        const data = await response.json();
        // Store token and role in localStorage
        localStorage.setItem('token', data.token);
        localStorage.setItem('role', data.role);

        displayLogoutButton();
    } catch (error) {
        errorMsg.style.display = 'block';
        errorMsg.textContent = error.message;
    }
});
