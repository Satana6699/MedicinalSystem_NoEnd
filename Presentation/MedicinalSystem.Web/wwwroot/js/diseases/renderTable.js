﻿const apiBaseUrl = "/api/diseases";

function renderDiseases(diseases) 
{
    const container = document.getElementById("diseases-container");
    container.innerHTML = "";

    if (diseases.length === 0) 
    {
        container.innerHTML = `
        <p>No diseases found.</p>
        <button onclick="addEmptyRowDisease()" style="margin-left: 20px; padding: 5px 10px; background: #2e7d32; color: #fff; border: none; border-radius: 4px; cursor: pointer;">
            Add new Disease
        </button>`;
        return;
    }

    const table = document.createElement("table");
    
    const caption = document.createElement("caption");
    caption.innerHTML = `
        Diseases List
        <a href="javascript:void(0);" onclick="addEmptyRowDisease()" title="Add Item">
            <i class="bi bi-plus-square-fill"></i>
        </a>`;


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
                <a href="javascript:void(0);" onclick="editRowDisease(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="delete_and_infoDisease(this)" title="Delete Item">
                    <i class="bi bi-eye-fill"></i>
                </a>
            </td>
        </tr>
    `).join('');

    table.appendChild(thead);
    table.appendChild(tbody);
    container.appendChild(table);
}