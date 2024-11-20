
function showCreateModal() 
{
    const modal = document.getElementById("diseaseModal");
    const modalContent = modal.querySelector(".modal-content");

    modalContent.innerHTML = 
    `
        <h3>Create New Disease</h3>
        <p><strong>Name:</strong> <input type="text" id="new-disease-name" style="width: 100%;"></p>
        <p><strong>Duration:</strong> <input type="text" id="new-disease-duration" style="width: 100%;"></p>
        <p><strong>Symptoms:</strong> <textarea id="new-disease-symptoms" style="width: 100%;"></textarea></p>
        <p><strong>Consequences:</strong> <textarea id="new-disease-consequences" style="width: 100%;"></textarea></p>
        <button onclick="closeModal()">Cancel</button>
        <button onclick="createDisease()">Create</button>
    `;

    modal.style.display = "block";
}

async function createDisease() 
{
    const name = document.getElementById("new-disease-name").value.trim();
    const duration = document.getElementById("new-disease-duration").value.trim();
    const symptoms = document.getElementById("new-disease-symptoms").value.trim();
    const consequences = document.getElementById("new-disease-consequences").value.trim();

    if (!name || !duration || !symptoms || !consequences) 
    {
        alert("All fields are required.");
        return;
    }

    const newDisease = { name, duration, symptoms, consequences };

    try 
    {
        const response = await axios.post(apiBaseUrl, newDisease);

        if (response.status === 201) 
        {
            alert("Disease created successfully!");
            loadDiseases(); // Обновляем таблицу
        } 
        else 
        {
            alert("Failed to create disease.");
        }
    } 
    catch (error) 
    {
        console.error("Error creating disease:", error);
        alert("Error creating disease. Please try again.");
    }

    closeModal();
}

