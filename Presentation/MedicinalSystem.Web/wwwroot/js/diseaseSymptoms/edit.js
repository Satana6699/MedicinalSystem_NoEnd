function editRowDiseaseSymptom(editButton) {
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

        saveChangesSymptom(id, updatedData, row);
    } else {
        // Начало редактирования
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

        cells.forEach(cell => {
            if (cell.classList.contains('editable')) {
                makeEditable(cell); // Для редактируемых ячеек
            } else {
                cell.setAttribute('contenteditable', 'true');
            }
        });

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

function makeEditable(cell) {
    const currentValue = cell.innerText.trim();
    const dropdown = document.createElement('div');
    dropdown.className = 'dropdown-container';
    dropdown.style.display = 'block';  // Скрывать по умолчанию, будет показан при редактировании
    const searchInput = document.createElement('input');
    searchInput.className = 'search-input';
    searchInput.placeholder = 'Search...';
    dropdown.appendChild(searchInput);

    // Список симптомов или болезней
    const dropdownList = document.createElement('div');
    dropdownList.className = 'dropdown-list';
    dropdown.appendChild(dropdownList);

    // Загрузка данных симптомов/болезней
    const data = cell.dataset.type === 'symptom' ? symptomsData : diseasesData; // Пример данных

    data.forEach(item => {
        const div = document.createElement('div');
        div.className = 'dropdown-item';
        div.innerText = item.name;
        div.onclick = function () {
            cell.innerText = item.name;
            dropdown.remove();
        };
        dropdownList.appendChild(div);
    });

    cell.appendChild(dropdown);

    // Фильтрация элементов в выпадающем списке
    searchInput.addEventListener('input', function () {
        const filter = searchInput.value.toLowerCase();
        const items = dropdownList.getElementsByClassName('dropdown-item');
        Array.from(items).forEach(item => {
            const text = item.innerText.toLowerCase();
            item.style.display = text.includes(filter) ? '' : 'none';
        });
    });
}

async function saveChangesSymptom(id, updatedData, row) {
    try {
        await axios.put(`${apiBaseUrl}/${id}`, updatedData);
        row.classList.remove('editing');
        const cells = row.querySelectorAll('td[contenteditable]');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'false'));

        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Иконка редактирования
        editButton.title = "Edit";

        // Удаляем кнопку отмены
        const cancelButton = row.querySelector('.cancel-button');

        //Обновляем страницу после успешного сохранения
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
