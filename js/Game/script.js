let urlParams = new URLSearchParams(window.location.search);

// console.log(urlParams.get('mode'));

const HTML_BOARD = document.getElementById('tic-tac-toe');
const ELEMENT_TAG = 'div';
const CELL_CLASS_NAME = 'spot';

drawBoard(HTML_BOARD);

let cells = document.getElementsByClassName(CELL_CLASS_NAME);

let board = new Board(cells);

function drawBoard(root) {
    for (let i = 0; i < Board.COUNT_SPOTS; i++) {
        let cell = document.createElement(ELEMENT_TAG);

        cell.className = CELL_CLASS_NAME;

        root.appendChild(cell);
    }
}