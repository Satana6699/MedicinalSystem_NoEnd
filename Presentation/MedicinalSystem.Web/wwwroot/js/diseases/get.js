const apiBaseUrl = "/api/diseases";
let currentPage = 1; // Текущая страница
const itemsPerPage = 10; // Количество записей на странице

async function loadData(page = 1) {
    try {
        const nameFilter = document.getElementById("filter-name").value || "";

        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
                pageSize: itemsPerPage,
                name: nameFilter,
            },
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Болезни";
        const tableHead = `
            <tr>
                <th style="padding: 8px; text-align: left;">Имя</th>
                <th style="padding: 8px; text-align: left;">Продолжительность</th>
                <th style="padding: 8px; text-align: left;">Симтомы</th>
                <th style="padding: 8px; text-align: left;">Последстивя</th>
                <th style="padding: 8px; text-align: left;">Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td style="padding: 8px;" contenteditable="false">${item.name}</td>
                <td style="padding: 8px;" contenteditable="false">${item.duration}</td>
                <td style="padding: 8px;" contenteditable="false">${item.symptoms}</td>
                <td style="padding: 8px;" contenteditable="false">${item.consequences}</td>
                <td style="padding: 8px;">
                    <a class="edit-buttons" href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            </tr>
        `).join('');

        // Создание таблицы
        createTable(itemsLength, totalCount, page, tableTitle, tableHead, tableBody);
    } catch (error) {
        if (typeof response !== 'undefined' && typeof response.status !== 'undefined' && error.response.status === 401) {
            alert('Сначала требуется пройти авторизацию.');
            // Если код состояния 401, перенаправляем на страницу авторизации
            window.location.href = '/Home/Auth';
            return;
        }
        console.error("Error fetching symptoms:", error);
        document.getElementById("table-container").innerHTML =
            `<p>Error loading symptoms. Please try again later.</p>`;
    }
}

// Инициализация
loadData();