﻿const apiBaseUrl = "/api/familyMembers";
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
        const tableTitle = "Члены семьи";
        const tableHead = `
            <tr>
                <th>ФИО</th>
                <th>Дата рождения</th>
                <th>Гендер</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${item.name}</td>
                <td contenteditable="false" date-str=${item.dateOfBirth}>${ISODateInBaseDate(item.dateOfBirth)}</td>
                <td data-field="gender" data-gender-id="${item.gender.id}">${item.gender.name}</td>
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

function ISODateInBaseDate(ISO) {
    const date = new Date(ISO);
    return `${date.getDate().toString().padStart(2, '0')}.${(date.getMonth() + 1).toString().padStart(2, '0')}.${date.getFullYear()}`;
    // на всякий случай
    return `${date.getDate().toString().padStart(2, '0')}.${(date.getMonth() + 1).toString().padStart(2, '0')}.${date.getFullYear()} ${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}:${date.getSeconds().toString().padStart(2, '0')}`;
}

// Инициализация
loadData();