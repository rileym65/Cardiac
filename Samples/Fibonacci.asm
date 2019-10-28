	org	10
start	out	num1	; output initial 0
loop	out	num2	; output next number in sequence
	cla	num2	; get last output number
	sub     n987    ; see if at end
	tac	go	; jump if not
	hrs		; otherwise halt
go	cla	num2	; get last number
	sto	temp	; save for now
	add	num1	; add in previous number
	sto	num2	; becomes new number
	cla	temp	; retrieve last number
	sto	num1	; and store into num1
	jmp	loop	; loop back

num1	data	0
num2	data	1
temp	data	0
n987	data	900

	end	start


