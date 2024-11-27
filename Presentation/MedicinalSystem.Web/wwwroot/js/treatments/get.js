const apiBaseUrl = "/api/treatments";
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
                Authorization: `Bearer ${localStorage.getItem('token') }`,
            },
        });

        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Лечение";
        const tableHead = `
            <tr>
                <th>Болезнь</th>
                <th>Лекарство</th>
                <th>Доза</th>
                <th>Продолжительность (дней)</th>
                <th>Интервал (в часах)</th>
                <th>Инструкция</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${item.disease.name}</td>
                <td contenteditable="false">${item.medicine.name}</td>
                <td contenteditable="false">${item.dosage}</td>
                <td contenteditable="false">${item.durationDays}</td>
                <td contenteditable="false">${item.intervalHours}</td>
                <td contenteditable="false">${item.instructions}</td>
                <td class="actions">
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
        ERROR(error);
    }
}

// Инициализация
loadData();