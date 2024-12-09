function editRow(editButton) {
    const row = editButton.closest('tr');
    let cells = row.querySelectorAll('td');
    const isEditing = row.classList.contains('editing');

    if (isEditing) {
        // Сохранение изменений
        const id = row.dataset.id;
        const newdate = new Date();
        newdate.setHours(newdate.getHours() + 3);
        const updatedData = {
            id: id,
            medicineId: cells[0].dataset.medicineId,
            price: cells[1].innerText.trim(),
            date: newdate,
        };

        saveChanges(id, updatedData, row);
    } else {
        // Начало редактирования
        cells = row.querySelectorAll('td[contenteditable]');
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(Array.from(cells).map(cell => cell.innerText.trim()));
        cells.forEach(cell => cell.setAttribute('contenteditable', 'true'));
        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>'; // Иконка сохранения
        editButton.title = "Save";

        // Добавляем кнопку отмены
        const cancelButton = document.createElement('a');
        cancelButton.innerHTML = '<i class="bi bi-x-circle-fill"></i>'; // Иконка крестика
        cancelButton.title = "Cancel";
        cancelButton.className = "cancel-button";
        cancelButton.onclick = () => cancelEditing(row);
        row.querySelector('td:last-child').appendChild(cancelButton);
    }
}

async function saveChanges(id, updatedData, row) {
    try {
        await axios.put(`${apiBaseUrl}/${id}`, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
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
        if (cancelButton) cancelButton.remove();
    } catch (error) {
        console.error("Error saving changes:", error);
        alert("Failed to save changes. Please try again.");
    }
}