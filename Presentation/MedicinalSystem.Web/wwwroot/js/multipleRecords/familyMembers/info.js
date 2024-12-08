async function info(deleteButton) {
    const row = deleteButton.closest('tr');
    const item = await axios.get(apiBaseUrl + "/" + row.dataset.id, {
        headers:
        {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    });
    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");
    modalContent.innerHTML = `
        <h3>Детали пациента</h3>
        <p><strong>ФИО пациента:</strong> ${item.data.name}</p>
        <p><strong>Дата рождения: </strong> ${ISODateInBaseDate(item.data.dateOfBirth)}</p>
        <p><strong>Гендер:</strong> ${item.data.gender.name}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}