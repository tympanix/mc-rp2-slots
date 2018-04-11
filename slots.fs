1 BLOCK
WIPE
0 PP : install 7 2 DO CR ." Installing: " I . ." of 6"
1 PP I LOAD LOOP CR ." Done!" ;
FLUSH

2 BLOCK
WIPE
0 PP : on? DUP IOX@ AND = ; : spc? 511 IOX@ AND = ;
1 PP : ht 7 ; : hm 56 ; : hb 448 ; : vl 73 ;
2 PP : vr 292 ; : cd 273 ; : cu 84 ; : vm 146 ;
3 PP : var VARIABLE ; var $n var $r var $s var $special
4 PP CREATE $bar 12 CELLS ALLOT : bar CELLS $bar + ;
5 PP 0 0 bar ! 1 1 bar ! 3 2 bar ! 7 3 bar ! 15 4 bar !
6 PP 31 5 bar ! 63 6 bar ! 127 7 bar ! 127 8 bar !
7 PP var $ic var $iron var $gold var $dia var $spins
8 PP var $hold var $delay var $dlst var $export
9 PP 60 $hold ! 5 $delay ! 20 $dlst !
10 PP : welcome 4096 IOXSET 10 TICKS 4096 IOXRST ;
11 PP : start 8192 IOXSET 1024 IOXSET 16384 IOXSET ;
12 PP : end 16384 IOXRST 1024 IOXRST ;
13 PP : rstline 16384 IOXSET 10 TICKS 16384 IOXRST ;
14 PP CREATE $co 18 CELLS ALLOT : co CELLS $co + ;
15 PP $co 18 CELLS 0 FILL
FLUSH

3 BLOCK
WIPE
0 PP : inc 1 $n +! ;
1 PP : check 0 $n ! IOX@
2 PP ht on? IF inc THEN hm on? IF inc THEN
3 PP hb on? IF inc THEN vl on? IF inc THEN
4 PP vm on? IF inc THEN vr on? IF inc THEN
5 PP cd on? IF inc THEN cu on? IF inc THEN ;
6 PP : showlvl 4 IOXADDR ! $n @ bar @ IOXSET 3 IOXADDR ! ;
7 PP : hidelvl 4 IOXADDR ! 127 IOXRST $s @ 0= IF
8 PP 896 IOXRST THEN 3 IOXADDR ! ; 8 1 co ! 16 2 co !
9 PP 32 3 co ! 64 5 co ! 128 6 co ! 576 8 co !
10 PP : checkspc 0 $s !
11 PP 495 spc? IF 1 $s ! 1 $r +! 128 THEN
12 PP 341 spc? IF 1 $s ! 1 $r +! 256 THEN
13 PP 325 spc? IF 1 $s ! 1 $r +! 512 THEN
14 PP $s @ IF 4 IOXADDR ! 3 0 DO DUP IOXRST 10 TICKS
15 PP DUP IOXSET 10 TICKS LOOP DROP 3 IOXADDR ! THEN ;
FLUSH

4 BLOCK
WIPE
0 PP : cashin SORTSLOTS 0 DO I SORTSLOT@
1 PP DUP 0<> IF -ROT +
2 PP DUP 5359 = IF DROP 16 >= IF 16 I SORTPULL
3 PP 16 $ic +! 1 $r +! LEAVE THEN THEN
4 PP DUP -9 = IF DROP 4 >= IF 4 I SORTPULL
5 PP 4 $iron +! 1 $r +! LEAVE THEN THEN
6 PP DUP 2958 = IF DROP 1 >= IF 1 I SORTPULL
7 PP 1 $gold +! 2 $r +! LEAVE THEN THEN
8 PP DUP 6191 = IF DROP 1 >= IF 1 I SORTPULL
9 PP 1 $dia +! 5 $r +! LEAVE THEN THEN
10 PP ELSE THEN LOOP DROP ;
11 PP : po 5 IOXADDR ! IOX! 10 TICKS
12 PP 0 IOX! 3 IOXADDR ! ;
13 PP : cashout $n @ DUP 0<> IF $s @ IF 8 + THEN
14 PP DUP 1 OVER 1 - << po co @ $export +! THEN ;
FLUSH

5 BLOCK
WIPE
0 PP CREATE $roll 12 CELLS ALLOT : roll CELLS $roll + ;
1 PP CREATE $sroll 12 CELLS ALLOT : sroll CELLS $sroll + ;
2 PP $roll 9 CELLS 0 FILL $sroll 9 CELLS 0 FILL : update $s @ IF
3 PP 1 $n @ sroll +! 1 $special +! ELSE 1 $n @ roll +! THEN ;
4 PP : listen BEGIN $r @ 0 <= IF 3072 IOXSET BEGIN
5 PP 60 TICKS 0 SORTSLOT@ NIP NIP IF
6 PP cashin THEN 32768 on? IF ." iSlots Stopped!"
7 PP rstline EXIT THEN $r @ UNTIL
8 PP 3072 IOXRST welcome THEN 0SP
9 PP BEGIN BEGIN $dlst @ TICKS 512 on? UNTIL
10 PP start hidelvl 1 $spins +! 1 $r -!
11 PP $r @ 0 <= IF cashin THEN
12 PP $hold @ TICKS 8192 IOXRST $delay @ TICKS
13 PP check showlvl cashout update checkspc
14 PP end 0SP $r @ 0= UNTIL AGAIN ;

6 BLOCK
WIPE
0 PP : stats PAGE CR ." Total rolls: " $spins @ .
1 PP 9 0 DO CR I . ." rows: " I roll @ . LOOP
2 PP CR CR ." Total specials: " $special @ .
3 PP 9 0 DO CR I . ." special rows: " I sroll @ . LOOP
4 PP CR CR ." Import:" CR ." Iron: " $iron @ . CR ." IC: "
5 PP $ic @ . CR ." Gold: " $gold @ . CR ." Diamonds: " $dia @ .
6 PP CR ." Total value: " $iron @ 4 * $gold @ 32 * +
7 PP $dia @ 128 * + $ic @ + . ." $"
8 PP CR CR ." Export:" CR ." Total value: " $export @ . ." $"
9 PP CR CR ." Payout values: "
10 PP 17 0 DO I . BS ." ) " I co @ . I 8 = IF CR THEN LOOP CR ;
FLUSH
