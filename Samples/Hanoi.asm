	org	3
tos	DATA	031
loader	DATA	100
storer	DATA	600
r2ld	DATA	100 r2
r2	DATA	001
	DATA	000
five	DATA	005
	DATA	004
three	DATA	003
	DATA	002

	org	34
start	INP	32	; Get the number of disks from the cards
	INP	31	; Get the column ordering from the cards
	JMP	tower	; Call the tower solver
	HRS

tower	CLA	99	; Push the return address on the stack
	JMP	push
	CLA	three	; Fetch n from the stack
	JMP	stkref
	SUB	00	; Check for n=0
	TAC	towdone
	JMP	push	; Push n-1 for a recursive call
	CLA	three	; Get the first recursive order
	JMP	stkref
	STO	t1
	CLA	five
	SUB	t1
	JMP	push
	JMP	tower	; Make first recursive call
	JMP	pop
	CLA	three	; Get move to output
	JMP	stkref
	STO	t1
	OUT	t1
	CLA	three	; Get second recursive order
	JMP	stkref
	ADD	r2ld
	STO	t2
t2	CLA	00
	JMP	push
	JMP	tower	; Make second recursive call
	JMP	pop
	JMP	pop
towdone	JMP	pop
	STO	towret
towret	JMP	00
t1	data	0

	org	70
stkref	STO	refsav	; Replace the accumulator with the contents
	CLA	99	; of the stack indexed by the accumulator
	STO	refret
	CLA	refsav
	ADD	tos
	ADD	loader
	STO	ref
ref	CLA	00
refret	JMP	00
refsav	data	00

	org	80
pop	CLA	99	; Pop the stack into the accumulator
	STO	popret
	CLA	tos
	ADD	00
	STO	tos
	ADD	loader
	STO	popa
popa	CLA	00
popret	JMP	00
pshsav	data	00
	org	90
push	STO	pshsav	; Push the accumulator on to the stack
	CLA	tos
	ADD	storer
	STO	psha
	CLA	tos
	SUB	00
	STO	tos
	CLA	pshsav
psha	STO	00

	end	start


