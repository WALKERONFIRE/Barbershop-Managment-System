console.log("tables page");

// append delete button to each <tr> element
const table = document.getElementById("myTable");
const rows = table.getElementsByTagName("tr");
for (let i = 0; i < rows.length; i++) {
    const deleteButton = document.createElement("button");
    deleteButton.innerText = "Delete";
    deleteButton.onclick = function () {
        this.parentNode.parentNode.remove();
    }
    const cell = document.createElement("td");
    cell.appendChild(deleteButton);
    rows[i].appendChild(cell);
}

// delete button
const deleteRowBtns = document.querySelectorAll('.delete-row-btn');

deleteRowBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        const row = btn.parentNode.parentNode;
        row.parentNode.removeChild(row);
    });
});

