function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Информация о пользоввателе</h3>
        <p><strong>Пользователь:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Полное имя:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Роль:</strong> ${row.cells[2].innerText}</p>
        <button onclick="closeModal()">Закрыть</button>
        <button class="edit-buttons" onclick="deleteRow('${id}')">Удалить</button>
    `;

    modal.style.display = "block";
    const editButtons = document.querySelectorAll('.edit-buttons');
    if (localStorage.getItem('role') === 'Admin') {
        // Показать все кнопки редактирования
        editButtons.forEach(button => {
            button.style.display = 'inline-block';
        });
    } else {
        // Скрыть все кнопки редактирования
        editButtons.forEach(button => {
            button.style.display = 'none';
        });
    }
}
