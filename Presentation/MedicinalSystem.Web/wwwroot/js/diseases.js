const apiBaseUrl = "/api/diseases";

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

function renderDiseases(diseases) 
{
    const container = document.getElementById("diseases-container");
    container.innerHTML = "";

    if (diseases.length === 0) 
    {
        container.innerHTML = "<p>No diseases found.</p>";
        return;
    }

    const table = document.createElement("table");
    table.style.width = "80%";
    table.style.margin = "20px auto";
    table.style.borderCollapse = "collapse";
    table.style.boxShadow = "0 2px 8px rgba(0, 0, 0, 0.1)";
    table.style.borderRadius = "8px";
    table.style.overflow = "hidden";

    const caption = document.createElement("caption");
    caption.innerHTML = `
        Diseases List 
        <button onclick="showCreateModal()" style="margin-left: 20px; padding: 5px 10px; background: #2e7d32; color: #fff; border: none; border-radius: 4px; cursor: pointer;">
            Create New Record
        </button>`;
    caption.style.captionSide = "top";
    caption.style.fontSize = "1.5em";
    caption.style.fontWeight = "bold";
    caption.style.color = "#2e7d32";
    caption.style.marginBottom = "10px";
    caption.style.textAlign = "center";

    table.appendChild(caption);

    const thead = document.createElement("thead");
    thead.innerHTML = 
    `
        <tr>
            <th style="padding: 8px; text-align: left;">Name</th>
            <th style="padding: 8px; text-align: left;">Duration</th>
            <th style="padding: 8px; text-align: left;">Symptoms</th>
            <th style="padding: 8px; text-align: left;">Consequences</th>
            <th style="padding: 8px; text-align: left;">Actions</th>
        </tr>
    `;

    const tbody = document.createElement("tbody");
    tbody.innerHTML = diseases.map(disease => 
    `
        <tr data-id="${disease.id}">
            <td style="padding: 8px;" contenteditable="false">${disease.name}</td>
            <td style="padding: 8px;" contenteditable="false">${disease.duration}</td>
            <td style="padding: 8px;" contenteditable="false">${disease.symptoms}</td>
            <td style="padding: 8px;" contenteditable="false">${disease.consequences}</td>
            <td style="padding: 8px;">
                <a href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="showDetails(this)" title="Show Details">
                    <i class="bi bi-eye-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="deleteRow(this)" title="Delete Item">
                    <i class="bi bi-file-earmark-excel-fill"></i>
                </a>
            </td>
        </tr>
    `).join('');

    table.appendChild(thead);
    table.appendChild(tbody);
    container.appendChild(table);
}

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


function editRow(editButton) 
{
    const row = editButton.closest('tr');
    const cells = row.querySelectorAll('td[contenteditable]');
    const isEditing = row.classList.contains('editing');

    if (isEditing) 
    {
        // Save changes
        const id = row.dataset.id;
        const updatedData = 
        {
            id: id,
            name: cells[0].innerText.trim(),
            duration: cells[1].innerText.trim(),
            symptoms: cells[2].innerText.trim(),
            consequences: cells[3].innerText.trim()
        };

        saveChanges(id, updatedData, row);
    } 
    else 
    {
        // Start editing
        row.classList.add('editing');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'true'));
        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>'; // Change to save icon
        editButton.title = "Save";
    }
}

async function saveChanges(id, updatedData, row) 
{
    try 
    {
        await axios.put(`${apiBaseUrl}/${id}`, updatedData);
        row.classList.remove('editing');
        const cells = row.querySelectorAll('td[contenteditable]');
        cells.forEach(cell => cell.setAttribute('contenteditable', 'false'));

        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>'; // Change back to edit icon
        editButton.title = "Edit";
    } 
    catch (error) 
    {
        console.error("Error saving changes:", error);
        alert("Failed to save changes. Please try again.");
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

function deleteRow(deleteButton) 
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("diseaseModal");
    const modalContent = modal.querySelector(".modal-content");

    modalContent.innerHTML = 
    `
        <h3>Are you sure you want to delete the disease?</h3>
        <p><strong>Name:</strong> ${row.cells[0].innerText}</p>
        <p><strong>Duration:</strong> ${row.cells[1].innerText}</p>
        <p><strong>Symptoms:</strong> ${row.cells[2].innerText}</p>
        <p><strong>Consequences:</strong> ${row.cells[3].innerText}</p>
        <button onclick="closeModal()">Close</button>
        <button onclick="deleteDisease('${id}')">Delete</button>
    `;

    modal.style.display = "block";
}

async function deleteDisease(id)
{
    try {
        const response = await axios.delete(`${apiBaseUrl}/${id}`);
        if (response.status === 204) {
            const row = document.querySelector(`tr[data-id="${id}"]`);
            if (row) row.remove(); // Удаляем строку из таблицы
            alert("Disease has been deleted successfully.");
        } else {
            alert("Failed to delete disease.");
        }
    } catch (error) {
        console.error("Error deleting disease:", error);
        if (error.response) {
            // Ответ от сервера с ошибкой
            alert(`Server error: ${error.response.status} - ${error.response.statusText}`);
        } else {
            // Ошибка запроса (например, нет соединения)
            alert("Network error: Failed to delete disease.");
        }
    }

    // Закрываем модальное окно после удаления
    const modal = document.getElementById("diseaseModal");
    modal.style.display = "none";
}

loadDiseases();
