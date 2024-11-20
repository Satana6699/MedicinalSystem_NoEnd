
async function loadDiseases() 
{
    try 
    {
        const response = await axios.get(apiBaseUrl);
        renderDiseases(response.data);
    } 
    catch (error) 
    {
        console.error("Error fetching diseases:", error);
        document.getElementById("diseases-container").innerHTML =
            `<p>Error loading diseases. Please try again later.</p>`;
    }
}

function showDetails(detailsButton) 
{
    const row = detailsButton.closest('tr');
    const id = row.dataset.id;

    // Получаем подробности болезни по ID
    axios.get(`${apiBaseUrl}/${id}`)
        .then(response => 
        {
            const disease = response.data;
            displayModal(disease);
        })
        .catch(error => 
        {
            console.error("Error fetching disease details:", error);
            alert("Failed to load disease details.");
        });
}

function displayModal(disease) 
{
    const modal = document.getElementById("diseaseModal");
    const modalContent = modal.querySelector(".modal-content");

    modalContent.innerHTML = 
    `
        <h3>${disease.name}</h3>
        <p><strong>Duration:</strong> ${disease.duration}</p>
        <p><strong>Symptoms:</strong> ${disease.symptoms}</p>
        <p><strong>Consequences:</strong> ${disease.consequences}</p>
        <button onclick="closeModal()">Close</button>
    `;

    modal.style.display = "block";
}

function closeModal() 
{
    const modal = document.getElementById("diseaseModal");
    modal.style.display = "none";
}

loadDiseases();