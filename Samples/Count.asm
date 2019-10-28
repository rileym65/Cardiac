	org	10
start	cla	00	; get number 1
	sto	count	; store into count
loop	out	count   ; output number
	cla	count	; get count
	add	00	; increment it
	sto	count	; put it back
	sub	n11	; check for end
	tac	loop	; loop back if not done
	hrs	00	; halt

count	data	00
n11	data	11

	end	start
