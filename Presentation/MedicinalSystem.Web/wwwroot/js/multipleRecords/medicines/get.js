const apiBaseUrl = "/api/medicines";
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
        const tableTitle = "Препораты";
        const tableHead = `
            <tr>
                <th>Препорат</th>
                <th>Показания</th>
                <th>Противопоказания</th>
                <th>Производитель</th>
                <th>Упаковка</th>
                <th>Дозировка</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${item.name}</td>
                <td contenteditable="false">${item.indications}</td>
                <td contenteditable="false">${item.contraindications}</td>
                <td data-field="manufacturer" data-manufacturer-id="${item.manufacturer.id}">${item.manufacturer.name}</td>
                <td contenteditable="false">${item.packaging}</td>
                <td contenteditable="false">${item.dosage}</td>
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