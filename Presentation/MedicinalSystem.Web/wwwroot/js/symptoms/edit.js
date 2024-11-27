﻿function editRow(editButton) {
    const row = editButton.closest('tr');
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions')); // Исключаем столбец действий
    const isEditing = row.classList.contains('editing');

    if (isEditing) {
        // Сохранение изменений
        const id = row.dataset.id;
        const updatedData = {
            id: id,
            name: cells[0].innerText.trim()
        };

        saveChanges(id, updatedData, row);
    } else {
        // Начало редактирования
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

        cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать
        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>'; // Иконка сохранения
        editButton.title = "Save";

        // Добавляем кнопку отмены
        const cancelButton = document.createElement('a');
        cancelButton.innerHTML = '<i class="bi bi-x-circle-fill"></i>'; // Иконка крестика
        cancelButton.title = "Cancel";
        cancelButton.className = "cancel-button";
        cancelButton.onclick = () => cancelEditingSymptom(row);
        row.querySelector('td.actions').appendChild(cancelButton); // Кнопка отмены только в actions
    }
}

async function saveChanges(id, updatedData, row) {
    try {
        await axios.put(`${apiBaseUrl}/${id}`, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token') }`,
            },
        });
        row.classList.remove('editing');
        const cells = row.querySelectorAll('td[contenteditable]');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'false'));

        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Иконка редактирования
        editButton.title = "Edit";

        // Удаляем кнопку отмены
        const cancelButton = row.querySelector('.cancel-button');

        // Обновление страницы
        location.reload();

        if (cancelButton) cancelButton.remove();
    } catch (error) {
        console.error("Error saving changes:", error);
        alert("Failed to save changes. Please try again.");
    }
}

function cancelEditingSymptom(row) {
    const cells = row.querySelectorAll('td[contenteditable]');
    const originalData = JSON.parse(row.dataset.originalData);

    // Возвращаем исходные значения
    cells.forEach((cell, index) => {
        cell.innerText = originalData[index];
        cell.setAttribute('contenteditable', 'false');
    });

    row.classList.remove('editing');

    // Убираем кнопки сохранения и отмены
    const editButton = row.querySelector('a[title="Save"]');
    if (editButton) {
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Иконка редактирования
        editButton.title = "Edit";
    }

    const cancelButton = row.querySelector('.cancel-button');
    if (cancelButton) cancelButton.remove();
}