	org	3
start	inp	arg1
	inp	arg2

	cla	arg1
	sto	a1
	cla	arg2
	sft	22
	sto	a2
	jmp	mult

	cla	arg1
	sft	10
	sto	a1
	cla	arg2
	sft	12
	sto	a2
	jmp	mult

	cla	arg1
	sft	20
	sto	a1
	cla	arg2
	sft	02
	sto	a2
	jmp	mult

	out	r1
	hrs

a1	data	0
a2	data	0
r1	data	0
mult	cla	99	; get return
	sto	mltret	; store for return
multlp	cla	a2
	sub	00	; decrement
	sto	a2	; store
	tac	mltret	; jump if done
	cla	a1	; get number
	add	r1	; add into total
	sto	r1	; put back into total
	jmp	multlp	; loop back
mltret	data	000

arg1	data	0
arg2	data	0
	run	start

	data	5
	data	123

