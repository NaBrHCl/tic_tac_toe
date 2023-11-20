const HTML_BOARD = document.getElementById('tic-tac-toe');
const ELEMENT_TAG = 'div';
const CELL_CLASS_NAME = 'spot';

let urlParams = new URLSearchParams(window.location.search);

let gameMode = urlParams.get('mode');

drawBoard(HTML_BOARD);

let cells = document.getElementsByClassName(CELL_CLASS_NAME);
let turnDisplayer = document.getElementById('turn-displayer');
let resultDisplayer = document.getElementById('result-displayer');

let board = new Board(cells, gameMode, turnDisplayer, resultDisplayer);

// createCallbacks(board, gameMode);

function drawBoard(root) {
    for (let i = 0; i < Board.COUNT_SPOTS; i++) {
        let cell = document.createElement(ELEMENT_TAG);

        cell.className = CELL_CLASS_NAME;

        root.appendChild(cell);
    }
}

function createCallbacks(board, mode) {
    for (let i = 0; i < board.spotElements.length; i++) {
        let spotCallback = (mode === 'PVP') ? board.getSpotCallback() : board.getComputerSpotCallback();

        board.spotCallbacks.push(spotCallback);

        board.spotElements[i].addEventListener('click', spotCallback);
    }
}