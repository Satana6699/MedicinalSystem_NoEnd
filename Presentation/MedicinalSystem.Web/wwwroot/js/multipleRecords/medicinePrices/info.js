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
        <h3>${item.data.medicine.name} ${item.data.price}р</h3>
        <p><strong>Имя препората:</strong> ${item.data.medicine.name}</p>
        <p><strong>Цена препората: </strong> ${item.data.price}</p>
        <p><strong>Дата обновления цены:</strong> ${ISODateInBaseDate(item.data.date)}</p>
        <button onclick=\"closeModal()\">Close</button>
        <a href="/Tables/MultipleRecords/Medicines/"">
            <button style="padding:cursor: pointer;">
                Удалять по ссылке
            </button>
        </a>
    `;

    modal.style.display = "block";
}