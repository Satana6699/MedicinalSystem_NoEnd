async function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");
    try {
        const token = localStorage.getItem('token');
        const response = await axios.get(apiBaseUrl + "/" + id, {
            headers:
            {
                Authorization: `Bearer ${token}`,
            },
        });

        const symptoms = response.data.symptoms; 
        const symptomsString = symptoms.map(symptom => symptom.name).join(', ');

        modalContent.innerHTML =
            `
        <h3>Дополнительная информация</h3>
        <p><strong>Болезнь: </strong> ${row.cells[0].innerText}</p>
        <p><strong>Продолжительность: </strong> ${row.cells[1].innerText}</p>
        <p><strong>Симтомы: </strong> ${symptomsString}</p>
        <p><strong>Последствия: </strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModal()">Close</button>
        <button onclick="deleteRow('${id}')">Delete</button>
    `;

        modal.style.display = "block";
    }
    catch (error) {
        console.error("Error:", error);
    }
}
