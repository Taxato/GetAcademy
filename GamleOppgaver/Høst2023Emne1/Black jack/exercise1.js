/*
Du skal lage en funksjon countBlackJackPoints(cards) som tar en array av kort som parameter og returnerer total verdi av disse kortene som en black jack-hånd.

Hvert kort er en tekst (string) som består av to ord, for eksempel 'Hjerter Konge'. Det er alltid et mellomrom i midten. Det første ordet er kortslag og er alltid en av følgende verdier: Hjerter, Spar, Ruter, Kløver.

Det andre ordet er enten et tall eller et av følgende ord: Knekt, Dame, Konge, Ess. Tallene er alltid 2, 3, 4, 5, 6, 7, 8, 9 eller 10.

Funksjonen er ikke ferdig. Du er fri til å gjøre den om som du vil, men funksjonen skal skrives slik at alle enhetstestene under passerer, dvs. blir godkjent. Du kan godt sette deg inn i black jack-reglene for å forstå bedre - men "fasiten" for hvordan koden skal oppføre seg er uansett enhetstestene som er definert her:

Funksjonen skal ikke aksessere noen globale variabler
Funksjonen skal ikke lage HTML av noe slag og ikke aksessere document i det hele tatt.
*/

/**
 * @param {string[]} cards
 * @returns {number} Total point value of card
 */
function countBlackJackPoints(cards) {
	if (!Array.isArray(cards)) return; //Make sure input is array

	let points = 0;
	let aceCount = 0;

	for (const card of cards) {
		const suitAndRank = card.split(" ");
		const rank = suitAndRank[1];
		const rankValue = parseInt(rank);

		if (!isNaN(rankValue)) points += rankValue;
		else if (rank === "Ess") {
			points += 11;
			aceCount++;
		} else points += 10;
	}

	while (points > 21 && aceCount > 0) {
		points -= 10;
		aceCount--;
	}

	return points;
}
