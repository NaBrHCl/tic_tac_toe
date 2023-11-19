const SIDE_LENGTH = 3;
const COUNT_SPOTS = SIDE_LENGTH ** 2;

const PLAYERS = ['✕', '〇'];

const INCREMENTS = [1, 3, 2, 4];
const STARTS = [[0, 3, 6], [0, 1, 2], [2], [0]];

class Board {
    static get SIDE_LENGTH() {
        return SIDE_LENGTH;
    }

    static get COUNT_SPOTS() {
        return COUNT_SPOTS;
    }

    constructor(spotElements) {
        this.spots = [];

        for (let i = 0; i < COUNT_SPOTS; i++)
            this.spots.push(undefined);

        this.spotElements = Array.from(spotElements);

        for (let i = 0; i < this.spotElements.length; i++) {

            this.spotElements[i].addEventListener('click', this.getSpotCallback(i));
        }

        this.currentPlayer = 0;
    }

    getSpotCallback(index) {
        return () => {
            if (this.tryPlacement(index)) {
                this.updateSpot(index);

                let result = this.checkWin();

                if (result !== undefined) {
                    for (let i = 0; i < this.spots.length; i++) {
                        console.log(result);

                        if (this.spots[i] === undefined)
                            this.spotElements[i].removeAllEventListener();
                    }

                    console.log('final state: ' + result);
                }
            }
        };
    }

    removeAllEventListener() {
        for (let i = 0; i < this.spots.length; i++) {
            if (this.spots[i] === undefined)
                this.spotElements[i].removeEventListener('click');
        }
    }

    tryPlacement(index) {
        if (this.spots[index] === undefined) {
            this.spots[index] = this.currentPlayer;

            this.changeCurrentPlayer();

            return true;
        }

        return false;
    }

    changeCurrentPlayer() {
        this.currentPlayer = (this.currentPlayer === 0) ? 1 : 0;
    }

    updateSpot(index) {
        this.spotElements[index].innerHTML = PLAYERS[this.spots[index]];
    }

    checkWin() {
        let hasEmptySpot = false;

        for (let i = 0; i < INCREMENTS.length; i++) { // for each direction (row, column, diagonal)
            for (let j = 0; j < STARTS[i].length; j++) { // for each starting position
                let player1Win = true;
                let player2Win = true;

                for (let k = 0; k < SIDE_LENGTH; k++) { // for each spot on the line
                    let spot = this.spots[STARTS[i][j] + k * INCREMENTS[i]];

                    if (spot === undefined) {
                        player1Win = false;
                        player2Win = false;
                        hasEmptySpot = true;
                        break;
                    }

                    if (spot === 0)
                        player2Win = false;
                    else
                        player1Win = false;
                }

                if (player1Win)
                    return 0;

                if (player2Win)
                    return 1;
            }
        }

        if (hasEmptySpot) // not finished
            return undefined;

        return -1; // draw
    }
}