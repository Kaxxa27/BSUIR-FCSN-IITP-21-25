let table = document.getElementById('table');
let CountInput = document.getElementById('sizeTable')

let addRowBtn = document.getElementById('add_row_btn'); 
let addColBtn = document.getElementById('add_col_btn'); 

let maxSelectionInput = document.getElementById('maxSelection');
if(maxSelectionInput.value === "")
{
    maxSelectionInput.value = 5;
}

let maxSelection = parseInt(maxSelectionInput.value) || 1;
let generateBtn = document.getElementById('generate_btn');
let transportajBtn = document.getElementById('transpontaj_btn');

let selectedCells = [];

generateBtn.addEventListener("click", generateTable);
transportajBtn.addEventListener("click", transposeTable);
addRowBtn.addEventListener("click", addRow);
addColBtn.addEventListener("click", addColumn);

function generateTable() {
    clearTable();
    let rowCount = parseInt(CountInput.value) || 0;
    let colCount = parseInt(CountInput.value) || 0;

    for (let i = 0; i < rowCount; i++) {
        let row = table.insertRow(i);
        for (let j = 0; j < colCount; j++) {
            let cell = row.insertCell(j);
            cell.innerText = Math.floor(Math.random() * 100) + 1;
            cell.addEventListener('click', () => selectCell(cell));
        }
    }
}

function transposeTable() {
    let newTable = document.createElement('table');
    for (let i = 0; i < table.rows[0].cells.length; i++) {
        let row = newTable.insertRow(i);
        for (let j = 0; j < table.rows.length; j++) {
            let cell = row.insertCell(j);
            cell.innerText = table.rows[j].cells[i].innerText;
            cell.addEventListener('click', () => selectCell(cell));
        }
    }
    clearTable();
    table.parentNode.replaceChild(newTable, table);
    table = newTable;
}


function selectCell(cell) {
    let maxSelection = parseInt(maxSelectionInput.value) || 1;
    if (selectedCells.length < maxSelection && !isAdjacent(cell)) {
        selectedCells.push(cell);

         // Проверяем четность значения в ячейке и добавляем соответствующий класс стиля
         if (parseInt(cell.innerText) % 2 === 0) {
            cell.classList.add('selected-even');
        } else {
            cell.classList.add('selected-odd');
        }
        
    } else {
        alert('Выделение невозможно. Превышено максимальное количество или ячейки соседние.');
    }
}

function isAdjacent(cell) {
    for (let selectedCell of selectedCells) {
        if (cell === selectedCell) return true;
        let selectedRow = selectedCell.parentNode.rowIndex;
        let selectedCol = selectedCell.cellIndex;
        let row = cell.parentNode.rowIndex;
        let col = cell.cellIndex;

        // Проверяем, что ячейки соседствуют только по горизонтали или вертикали
        if (
            (Math.abs(selectedRow - row) === 1 && selectedCol === col) ||
            (selectedRow === row && Math.abs(selectedCol - col) === 1)
        ) {
            return true;
        }
    }
    return false;
}


function clearTable() {
    while (table.rows.length > 0) {
        table.deleteRow(0);
    }
    selectedCells = [];
}

function addRow() {
    let newRow = table.insertRow(table.rows.length);
    let colCount = table.rows[0].cells.length;
    for (let j = 0; j < colCount; j++) {
        let cell = newRow.insertCell(j);
        cell.innerText = Math.floor(Math.random() * 100) + 1;
        cell.addEventListener('click', () => selectCell(cell));
    }
}

function addColumn() {
    let rowCount = table.rows.length;
    for (let i = 0; i < rowCount; i++) {
        let cell = table.rows[i].insertCell(table.rows[i].cells.length);
        cell.innerText = Math.floor(Math.random() * 100) + 1;
        cell.addEventListener('click', () => selectCell(cell));
    }
}