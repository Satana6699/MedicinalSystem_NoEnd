function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Are you sure you want to delete the disease?</h3>
        <p><strong>Name:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Duration:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Symptoms:</strong> ${row.cells[2].innerText}</p>
        <p><strong>Consequences:</strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModal()">Close</button>
        <button onclick="deleteRow('${id}')">Delete</button>
    `;

    modal.style.display = "block";
}
