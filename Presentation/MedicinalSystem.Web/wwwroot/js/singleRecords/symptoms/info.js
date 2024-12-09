function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Are you sure you want to delete the symptom?</h3>
        <p><strong>Симптом:</strong> ${row.cells[0].innerText}</p>
        <button onclick="closeModal()">Close</button>
        <button class="edit-buttons" onclick="deleteRow('${id}')">Delete</button>
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
