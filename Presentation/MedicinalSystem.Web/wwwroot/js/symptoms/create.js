﻿function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cell = row.querySelector("td[contenteditable]");

    // Собираем данные из строки
    const newSymptom = {
        name: cell.innerText.trim()
    };

    // Проверяем заполненность поля
    if (!newSymptom.name) {
        alert("The name field must be filled.");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newSymptom, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token') }`,
            },
        });

        if (response.status === 201) {
            alert("Данные созданы успешно!");

            // Обновляем строку с новыми данными
            row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            row.innerHTML = `
                <td style="padding: 8px;" contenteditable="false">${response.data.name}</td>
                <td style="padding: 8px;">
                    <a href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                        <i class="bi bi-pencil-fill"></i>
                    </a>
                    <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
                        <i class="bi bi-eye-fill"></i>
                    </a>
                </td>
            `;
        } else {
            throw new Error("Failed to create Symptom.");
        }
    } catch (error) {
        console.error("Error creating Symptom:", error);
        alert("Failed to create Symptom. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}
