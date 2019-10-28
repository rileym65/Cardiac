	org	4
n	data	000
cntr	data	009

	org	10
start	cla	00	; Initialize the power variable with 2^0
	jmp	aprint
loop	sto	n
	cla	cntr	; Decrement the counter
	sub	00
	tac	exit	; Are we done yet?
	sto	cntr
	cla	n
	jmp	double	; Double the power variable
	jmp	aprint	; print it
	jmp	loop
exit	hrs	00

	org	80
aprint	sto	86	; Print a card with the contents of the accumulator
	cla	99
	sto	aexit
	out	86
	cla	86
aexit	jmp	00

	org	90
double	sto	96	; Double the contents of the accumulator
	cla	99
	sto	dexit
	cla	96
	add	96
dexit	jmp	00

	end	start

