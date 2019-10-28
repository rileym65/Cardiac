[c]	CARDIAC
[c]	CARDboard Illustrative Aid to Computation
[c]	by
[c]	Mike Riley
[=]
	CARDIAC was developed by Bell Telephone Laboratories in 1968 as an inexpensive
	means to introduce the concepts of how computers work to high school students.
	It was essentially a cardboard "computer".  Although it is not technically a
	computer it does a very good job of explaining how a computer actually works as
	well as facilitate learning to program without the need for expensive hardware.
	The CARDIAC provided instructions to the user on how to perform the various
	actions of a computer and it was up to the user to actually move numbers around,
	by writing them in pencil in various sqaures, as well as to perform any actual
	calculation.
[=]
	The simulated computer of the CARDIAC consisted of 100 memory cells, each of which
	held a 3 digit decimal number.  It also contained a 3 digit Accumulator and a 
	3 digit Instruction Register.  It also contained a simulated card reader and punch.
	The CARDIAC defined 10 different instructions.
[=]
	The user would manipulate 3 slides to place the current instruction
	into the Instruction Register and the CARDIAC would decode the instruction and 
	provide instructions to the user on how to perform the instruction.  So in essence
	the user was the "computer" and the CARDIAC operated as the control unit instructing
	the user on exactly what to do next.  All in all, a rather ingenious way of teaching
	computing at a time when computers were way out of reach of the average person.
[=]
[h2]	This Emulator
[=]
	This emulator attempts to, as faithfully as possible, simulate the original CARDIAC.
	When the emulator is operating in the "Classic" mode it requires the same type of
	interaction from the user as the original CARDIAC did, the user must manually move
	numbers around, advance cards, perform arithmetic, etc.  The "Classic" mode is 
	intended to not only simulate the original CARDIAC but also the user experience of
	using the CARDIAC.
[=]
	This emulator also provides an "Auto" mode.  In this mode the emulator performs
	automatically all the manual operations the CARDIAC required of the user.  In this
	mode the emulator is more like a "real" computer based on the CARDIAC model.  In the
	"Auto" mode programs can be single stepped or automatically continually execute
	instructions until a halting condition is encountered.
[=]
[h2]	Sample Run
[=]
	To help you understand how to use the CARDIAC, we will go through a sample run
	using a simple program that just adds two numbers together and outputs the
	result.
[=]
	Normally programs should be placed onto card strips and then use the "bootloader"
	process to load them, but for this sample we will go ahead and write the
	values directly into memory.  You will learn about the bootloader process later.
[=]
	Do not worry too much about what the program codes mean, we will dive deeper
	into that later.  For now you just need to put the correct values into memory.
	The table below shows you what value to place at each memory address:
[=]
[i10]
[tb]
	| Memory cell | Value |
	| ----------- | ----- |
	| 03          | 010   |
	| 04          | 011   |
	| 05          | 110   |
	| 06          | 211   |
	| 07          | 612   |
	| 08          | 512   |
	| 09          | 900   |
[te]
[i-10]
[=]
	This program expects to read two values from input cards, so lets set that up
	next.  We need to create a card strip for the input.  Click on the "Input/Output"
	tab.  In the Input Cards section, in the box, put 004 and 005 on two separate lines.
	Then click on the "Load Reader" button to insert the card strip into the CARDIAC
	input reader.
[=]
	The last thing we need to do before "running" our program is to setup the program
	counter or "bug" to the starting point of our program which is cell 03.  So click
	on the circle following the 01 in the memory cells.
[=]
	Note:  The original CARDIAC called the program counter a "bug," this was because it
	came with a cardboard ladybug that was used to mark the current program step.  So 
	in this documentation when referring to the "bug" this means the program counter.
[=]
	Our computer is now all set to run our sample program.  Just follow the steps below:
[=]
	1. Move the bug to cell 03
[=]
	2. Now we start at the "Start" portion which has an arrow pointing to the
	instruction register.  Here we are instructed to move the slides to agree
	with the contents of the bug's cell.  The bug is on cell 03 and the contents are
	010, so move the slides to show 0 1 0.
[=]
	3. Now follow the arrow to the box that says "Move bug ahead one cell", we now
	follow this instruction by moving the bug from cell 03 to cell 04.
[=]
	4. Now follow the arrow to the Accumulator Test box, which shows the question
	"Is input card blank?"  Since the input card is not blank we follow the arrow
	above the "No" option.
[=]
	5. This puts us at the instruction decode window which instructs us to "Copy
	input card into cell 10, advance card."  The input card contains 004, so write
	004 into cell 10.  Then advance the card by clicking on the down arrow below the
	input window.
[=]
	6. Follow the arrow out back to the Instruction Register.  We have now completed
	1 complete machine cycle.
[=]
	7. Now the bug is on cell 04, which contains 011, so move the slides to set 0 1 1
	into the instruction register.
[=]
	8. Follow the arrow, to the move bug box, follow the instruction by moving the bug
	from cell 04 to cell 05.
[=]
	9. Now to the Accumulator Test window.  The input card is not blank, so follow the
	No arrow to the instruction decode window.
[=]
	10. The instruction is telling us to copy the input card to cell 11, so copy the 005
	from the input card to cell 11 and then advance the card.
[=]
	11. Now back at the Instruction Register, with the bug on cell 05, which contains 110,
	move the slides to show 1 1 0 in the register.
[=]
	12. Now the move bug box, move the bug from cell 05 to cell 06.
[=]
	13. The Accumulator test window now just contains a continuation of the arrow, so 
	proceed directly to the instruction decode window.
[=]
	14. The window contains two actions for us,  First erase the Accumulator, and then
	to copy the contents of cell 10 into the accumulator.  Cell 10 contains 004 at this
	time, so put 0 0 4 into the lowest three boxes of the accumulator.

[=]
	15. Back to the Instruction Register.  Cell 06 contains 211, so set 2 1 1 on the slides.
[=]
	16. Next we are at the box that tells us to move the bug, so move it from 06 to 07.
[=]
	17. The Accumulator Test window just shows a continuation of the arrow, so proceed
	directly to the instruction decode window.
[=]
	18. This window tells us to add the contents of cell 11 to the Accumulator.  Although
	this is a really simple example that you can add easily in your head, we will use the
	workspace anyways for when you get to larger numbers.  Take the accumulator contents
	of 0 0 4 from the bottom row and put them into the top row. Cell 11 contains 005, so
	put 0 0 5 into the middle row.  Now use your grade school math to add the top and middle
	rows to give a new bottom row of 0 0 9.
[=]
	19. Back to the Instruction Register.  The bug is on cell 07, which contains 612, so set
	the slides to 6 1 2.
[=]
	20. Now to the move bug box, move the bug from cell 07 to 08.
[=]
	21. The Accumulator Test box just contains a continuation of the arrow, so on up to the
	instruction decode window.
[=]
	22. Now we are instructed to "Copy the accumulator into cell 12".  The bottom row of the
	Accumulator should have 0 0 9, so write 009 into cell 12.
[=]
	23. Forward to the Instruction Register.  The bug is now on cell 08, which contains 512,
	so set the slides to 5 1 2.
[=]
	24. Next to the move bug window, we now move the bug from cell 08 to cell 09.
[=]
	25. The Accumulator Test window just has a continuation of the arrow, so on up to the
	instruction decode window.
[=]
	26. We encounter the instructions "Copy contents of cell 12 to output card and advance
	card".  So, copy the contents of cell 12, which should be 009, to the output window and
	then click the arrow below the output window to advance the card.
[=]

	27. On to the Instruction Window.  The bug is on cell 09, which contains 900, so set the
	slides to 9 0 0.
[=]
	28. Next is the move bug window, move the bug from cell 09 to 10.
[=]
	29. The Accumulator Test window just shows a continuation of the arrow so on to the
	Instruction Decode window.
[=]
	30. The instruction "Move bug to cell 00, stop" appears.  Move the bug from cell 10 to
	cell 00.  And since we encountered the "stop" command, the program is complete.
[=]
[h2]	Bootloader
[=]
	In the above sample session we loaded our program directly into memory.  In most
	computers this is not possible, instead there is a boot process for loading a
	program into memory.  CARDIAC also has the ability through the input card reader to
	be able to load and execute a program.  
[=]
	In order to create a card strip that is suitable for bootloading you must have the first
	two cards as:
[=]
[-]	002
[-]	800
[=]
	The 002 card is a 'INP 002' instruction which would cause the next card, the 800
	card, to be read and placed into address 02.  The 800 is a JMP 00 instruction which
	would cause a jump back to cell 00, which always contains 001, a 'INP 01' command.
[=]
	From this point on, you use two cards for each value to load into memory, the first
	card would be the address to load and the second card would be the value to load at
	that address.  So for our example program above, the next group of cards would be:
[=]
[-]	003
[-]	010
[-]	004
[-]	011
[-]	005
[-]	110
[-]	006
[-]	211
[-]	007
[-]	612
[-]	008
[-]	512
[-]	009
[-]	900
[=]
	The last thing you need is a way to get out of the load process and start the
	actual program.  This is accomplished by having a 002 card followed by a 8XX 
	card where XX is replaced with the address to begin execution.  So for our sample
	program, the last two cards in the stack would be:
[=]
[-]	002
[-]	803
[=]
	You can try taking all these cards and creating a card strip in the emulator and
	then run from address 00 and see exactly how the whole load process works.
[=]
	Another useful ending card would be 9XX which would cause the bug to be moved to the
	beginning of the executable code but then enter halt mode.  This is useful if you
	are running the emulator in auto mode and want to stop after the load process so that
	you can either enter Classic mode or single step for debugging reasons.
[=]
[h2]	Instructions
[=]
[tb]
	| opcode | Mnemonic | description |
	| 0XX | INP | Copy input card to cell XX, advance card                     |
	| 1XX | CLA | Erase Accumulator. Copy contents of cell XX into Accumulator |
	| 2XX | ADD | Add contents of cell XX into Accumulator                     |
	| 3xx | TAC | Move bug to cell XX on negative Accumulator                  |
	| 4LR | SFT | Shift Accumulator left L places, then right R places         |
        | 5XX | OUT | Copy contents of cell XX to output card and advance card     |
	| 6XX | STO | Copy Accumulator into cell XX                                |
	| 7XX | SUB | Subtract contents of cell XX from Accumulator                |
	| 8XX | JMP | Write bug's cell no. to cell 99, Move bug to cell XX         |
	| 9XX | HRS | Move bug to cell XX.  Stop.                                  |
[te]
[=]
[h2]	Assembler
[=]
	This emulator comes with a simple built in assembler to aid in preparing
	programs to run with the CARDIAC.
[=]
	The format for an assembly line is:
[=]
	label  MNEMONIC  address  ; comment
[=]
	The label, if specified, must begin at the beginning of the line, if there
	is any whitespace then it will not be recognized as a label.  Comments are
	preceeded with a semicolon (;) character and extend to the end of the line.
[=]
[h2]	Pseudo Ops
[=]
[tb]
	| label | EQU  | value | Set label equal to value          |
	|       | ORG  | value | Set assembly address to value     |
	|       | DATA | value | Value is set into current address |
	|       | END  | value | End and set start address         |
	|       | RUN  | value | End and set start address         |
	|       | HALT | value | End and set start address         |
[te]
[=]
	END and RUN are synonomous.  In both cases if the assembly output is
	to a card strip then the necessary cards for starting the program at
	the specified address will be added to the card strip.
[=]
	HALT is similar to END and RUN except that instead of instructions for
	for starting the program being added to the card strip, instructions to
	halt after load will be placed on the card strip.  This instruction is
	used when you want to halt the computer after the bootloading process
	has completed and before the program is executed so that you can single
	step the program.
[=]
	Assembly does not actually end when END, RUN, or HALT is specified, but
	rather the nature of how cards are written in card strip mode.  In normal
	assembly to cards, two cards are written for each instruction, the first 
	card contains the address and the second card contains the instruction.
	After END, RUN, or HALT is encounted then only a single card will be 
	written for each assembly line.  This allows you to create data cards
	on the output card strip following the program that the program when
	executed can read.
[=]
[h2]	Sample Programs
[=]
	Here are a couple sample programs to get you started in what you can
	do with CARDIAC.  Each sample program will be given in three forms:
	1) Assembly Source, which is the source you would use with the built in
	assembler, 2) Memory Image, which is a list of addresses and values that
	can be entered directly into CARDIAC's memory cells, and 3) Cards, a list
	cards that are in bootloader format.
[=]
[h1]	Count
[=]
	This sample simply outputs the number from 1 to 10.
[=]
	Assembly source
[=]
[tb]
	| ####### | #### | ##### | ####################### |
	|         | org  | 10    |                         |
	| start   | cla  | 00    | ; get number 1          |
	|         | sto  | count | ; store into count      |
	| loop    | out  | count | ; write count to card   |
	|         | cla  | count | ; get count             |
	|         | add  | 00    | ; increment it          |
	|         | sto  | count | ; put it back           |
	|         | sub  | n11   | ; check for end         |
	|         | tac  | loop  | ; loop back if not done |
	|         | hrs  | 00    | ; halt                  |
	| count   | data | 00    |                         |
	| n11     | data | 011   |                         |
	|         | end  | start |                         |
[te]
[=]
	Memory image
[=]
[tb]
	| 10 | 100 |
	| 11 | 619 |
	| 12 | 519 |
	| 13 | 119 |
	| 14 | 200 |
	| 15 | 619 |
	| 16 | 720 |
	| 17 | 312 |
	| 18 | 900 |
	| 19 | 000 |
	| 20 | 011 |
[te]
[=]
	Cards
[=]
[-]	002
[-]	800
[-]	010
[-]	100
[-]	011
[-]	619
[-]	012
[-]	519
[-]	013
[-]	119
[-]	014
[-]	200
[-]	015
[-]	619
[-]	016
[-]	720
[-]	017
[-]	312
[-]	018
[-]	900
[-]	019
[-]	000
[-]	020
[-]	011
[-]	002
[-]	810
[=]
[h1]	Fibonacci Sequence
[=]
	This sample outputs the Fibonacci sequence below 1000.
[=]
	Assembly source
[=]
[tb]
	|          | org    | 10    |                                  |
	| start    | out    | num1  | ; output initial 0               |
	| loop     | out    | num2  | ; output next number in sequence |
	|          | cla    | num2  | ; get last output number         |
	|          | sub    | n900  | ; see if at end                  |
	|          | tac    | go    | ; jump if not                    |
	|          | hrs    | 0     | ; otherwise halt                 |
	| go       | cla    | num2  | ; get last number                |
	|          | sto    | temp  | ; save for now                   |
	|          | add    | num1  | ; add in previous number         |
	|          | sto    | num2  | ; becomes new number             |
	|          | cla    | temp  | ; retrieve last number           |
	|          | sto    | num1  | ; and store into num1            |
	|          | jmp    | loop  | ; loop back                      |
	| num1     | data   | 000   |                                  |
	| num2     | data   | 001   |                                  |
	| temp     | data   | 000   |                                  |
	| n900     | data   | 900   |                                  |
	|          | end    | start |                                  |
[te]
[=]
	Memory image
[=]
[tb]
	| 10 | 523 |
	| 11 | 524 |
	| 12 | 124 |
	| 13 | 726 |
	| 14 | 316 |
	| 15 | 900 |
	| 16 | 124 |
	| 17 | 625 |
	| 18 | 223 |
	| 19 | 624 |
	| 20 | 125 |
	| 21 | 623 |
	| 22 | 811 |
	| 23 | 000 |
	| 24 | 001 |
	| 25 | 000 |
	| 26 | 900 |
[te]
[=]
	Cards
[=]
[-]	002
[-]	800
[-]	010
[-]	523
[-]	011
[-]	524
[-]	012
[-]	124
[-]	013
[-]	726
[-]	014
[-]	316
[-]	015
[-]	900
[-]	016
[-]	124
[-]	017
[-]	625
[-]	018
[-]	223
[-]	019
[-]	624
[-]	020
[-]	125
[-]	021
[-]	623
[-]	022
[-]	811
[-]	023
[-]	000
[-]	024
[-]	001
[-]	025
[-]	000
[-]	026
[-]	900
[-]	002
[-]	810
[=]
