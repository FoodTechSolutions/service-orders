°*
%D:\service-orders\src\DOMAIN\Order.cs
	namespace 	
DOMAIN
 
; 
public 
class 
Order 
: 
AggregateRoot "
{ 
public		 

Order		 
(		 
Guid		 

customerId		  
,		  !
double		" (
discount		) 1
,		1 2
OrderStatus		3 >
status		? E
)		E F
{

 

CustomerId 
= 

customerId 
;  
Discount 
= 
discount 
; 
Status 
= 
status 
; 
} 
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Computed/ 7
)7 8
]8 9
public 

int 
Number 
{ 
get 
; 
private $
set% (
;( )
}* +
[ 

ForeignKey 
( 
$str 
) 
] 
public 

Guid 

CustomerId 
{ 
get  
;  !
private" )
set* -
;- .
}/ 0
public 

double 
Discount 
{ 
get  
;  !
private" )
set* -
;- .
}/ 0
public 

OrderStatus 
Status 
{ 
get  #
;# $
private% ,
set- 0
;0 1
}2 3
public 

bool 
IsPaid 
{ 
get 
; 
private %
set& )
;) *
}+ ,
=- .
false/ 4
;4 5
public 

List 
< 
OrderProduct 
> 
Products &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 

void 

MarkAsPaid 
( 
) 
{ 
IsPaid 
= 
true 
; 
} 
public   

Order   
	SetIsPaid   
(   
bool   
isPaid    &
)  & '
{!! 
IsPaid"" 
="" 
isPaid"" 
;"" 
return## 
this## 
;## 
}$$ 
public&& 

Order&& 
AddProducts&& 
(&& 
IEnumerable&& (
<&&( )
OrderProduct&&) 5
>&&5 6
products&&7 ?
)&&? @
{'' 
if(( 

((( 
Products(( 
is(( 
null(( 
)(( 
Products(( &
=((' (
new(() ,
List((- 1
<((1 2
OrderProduct((2 >
>((> ?
(((? @
)((@ A
;((A B
Products)) 
.)) 
AddRange)) 
()) 
products)) "
)))" #
;))# $
return** 
this** 
;** 
}++ 
public-- 

static-- 
Order-- 
CreateOrder-- #
(--# $
Guid--$ (

customerId--) 3
,--3 4
double--5 ;
discount--< D
,--D E
OrderStatus--F Q
status--R X
)--X Y
{.. 
return// 
new// 
Order// 
(// 

customerId// #
,//# $
discount//% -
,//- .
status/// 5
)//5 6
;//6 7
}00 
public22 

Order22 
MoveToNextStep22 
(22  
)22  !
{33 
Status44 
=44 
GetNextStatus44 
(44 
Status44 %
)44% &
;44& '
return55 
this55 
;55 
}66 
public88 

bool88 
IsLastStatus88 
(88 
)88 
=>99 

Status99 
==99 
OrderStatus99  
.99  !
Finished99! )
;99) *
private;; 
OrderStatus;; 
GetNextStatus;; %
(;;% &
OrderStatus;;& 1
currentStatus;;2 ?
);;? @
{<< 
switch== 
(== 
currentStatus== 
)== 
{>> 	
case?? 
OrderStatus?? 
.?? 
Received?? %
:??% &
return@@ 
OrderStatus@@ "
.@@" #

InProgress@@# -
;@@- .
caseBB 
OrderStatusBB 
.BB 

InProgressBB '
:BB' (
returnCC 
OrderStatusCC "
.CC" #
ReadyCC# (
;CC( )
caseEE 
OrderStatusEE 
.EE 
ReadyEE "
:EE" #
returnFF 
OrderStatusFF "
.FF" #
FinishedFF# +
;FF+ ,
caseHH 
OrderStatusHH 
.HH 
FinishedHH %
:HH% &
returnII 
currentStatusII $
;II$ %
defaultKK 
:KK 
returnLL 
currentStatusLL $
;LL$ %
}MM 	
}NN 
}QQ Ï
1D:\service-orders\src\DOMAIN\Enums\OrderStatus.cs
	namespace 	
DOMAIN
 
. 
Enums 
; 
public 
enum 
OrderStatus 
{ 
Received 
= 
$num 
, 

InProgress 
= 
$num 
, 
Ready 	
=
 
$num 
, 
Finished 
= 
$num 
}		 Œ
?D:\service-orders\src\DOMAIN\Entities\OrderProductIngredient.cs
	namespace 	
DOMAIN
 
. 
Entities 
; 
public 
class "
OrderProductIngredient #
:$ %

BaseEntity& 0
{ 
public 

int 
Quantity 
{ 
get 
; 
set "
;" #
}$ %
public		 

static		 "
OrderProductIngredient		 (
CreateIngredient		) 9
(		9 :
int		: =
quantity		> F
)		F G
{

 
return 
new "
OrderProductIngredient )
{ 	
Quantity 
= 
quantity 
,  
} 	
;	 

} 
} í
5D:\service-orders\src\DOMAIN\Entities\OrderProduct.cs
	namespace 	
DOMAIN
 
. 
Entities 
; 
public 
class 
OrderProduct 
: 

BaseEntity &
{ 
public 

Guid 
	ProductId 
{ 
get 
;  
private! (
set) ,
;, -
}. /
public 

int 
Quantity 
{ 
get 
; 
private &
set' *
;* +
}, -
public		 

List		 
<		 "
OrderProductIngredient		 &
>		& '
Ingredients		( 3
{		4 5
get		6 9
;		9 :
set		; >
;		> ?
}		@ A
public 

static 
OrderProduct 
CreateProduct ,
(, -
Guid- 1
	productId2 ;
,; <
int= @
quantityA I
)I J
{ 
return 
new 
OrderProduct 
{  !
	ProductId" +
=, -
	productId. 7
,7 8
Quantity9 A
=B C
quantityD L
}M N
;N O
} 
public 

OrderProduct 
AddIngredients &
(& '
IEnumerable' 2
<2 3"
OrderProductIngredient3 I
>I J
ingredientsK V
)V W
{ 
if 

( 
Ingredients 
is 
null 
)  
Ingredients! ,
=- .
new/ 2
List3 7
<7 8"
OrderProductIngredient8 N
>N O
(O P
)P Q
;Q R
Ingredients 
. 
AddRange 
( 
ingredients (
)( )
;) *
return 
this 
; 
} 
} „
0D:\service-orders\src\DOMAIN\Base\ValueObject.cs
	namespace 	
DOMAIN
 
. 
Base 
; 
public 
class 
ValueObject 
{ 
} œ
/D:\service-orders\src\DOMAIN\Base\BaseEntity.cs
	namespace 	
DOMAIN
 
. 
Base 
; 
public 
abstract 
class 

BaseEntity  
{ 
[ 
Key 
] 	
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
=! "
Guid# '
.' (
NewGuid( /
(/ 0
)0 1
;1 2
public		 

DateTime		 
	CreatedAt		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
=		, -
DateTime		. 6
.		6 7
UtcNow		7 =
;		= >
public

 

DateTime

 
?

 
DeleteAt

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
} ”ê
5D:\service-orders\src\DOMAIN\Base\AssertionConcern.cs
	namespace 	
DOMAIN
 
. 
Base 
{ 
public 

class 
AssertionConcern !
{ 
public 
static 
void  
AssertArgumentEquals /
(/ 0
object0 6
object17 >
,> ?
object@ F
object2G N
,N O
stringP V
messageW ^
)^ _
{ 	
if 
( 
! 
object1 
. 
Equals 
(  
object2  '
)' (
)( )
{ 
throw 
new %
InvalidOperationException 3
(3 4
message4 ;
); <
;< =
} 
} 	
public 
static 
void 
AssertArgumentFalse .
(. /
bool/ 3
	boolValue4 =
,= >
string? E
messageF M
)M N
{ 	
if   
(   
	boolValue   
)   
{!! 
throw"" 
new"" %
InvalidOperationException"" 3
(""3 4
message""4 ;
)""; <
;""< =
}## 
}$$ 	
public&& 
static&& 
void&&  
AssertArgumentLength&& /
(&&/ 0
string&&0 6
stringValue&&7 B
,&&B C
int&&D G
maximum&&H O
,&&O P
string&&Q W
message&&X _
)&&_ `
{'' 	
int(( 
length(( 
=(( 
stringValue(( $
.(($ %
Trim((% )
((() *
)((* +
.((+ ,
Length((, 2
;((2 3
if)) 
()) 
length)) 
>)) 
maximum))  
)))  !
{** 
throw++ 
new++ %
InvalidOperationException++ 3
(++3 4
message++4 ;
)++; <
;++< =
},, 
}-- 	
public// 
static// 
void//  
AssertArgumentLength// /
(/// 0
string//0 6
stringValue//7 B
,//B C
int//D G
minimum//H O
,//O P
int//Q T
maximum//U \
,//\ ]
string//^ d
message//e l
)//l m
{00 	
int11 
length11 
=11 
stringValue11 $
.11$ %
Trim11% )
(11) *
)11* +
.11+ ,
Length11, 2
;112 3
if22 
(22 
length22 
<22 
minimum22  
||22! #
length22$ *
>22+ ,
maximum22- 4
)224 5
{33 
throw44 
new44 %
InvalidOperationException44 3
(443 4
message444 ;
)44; <
;44< =
}55 
}66 	
public88 
static88 
void88 !
AssertArgumentMatches88 0
(880 1
string881 7
pattern888 ?
,88? @
string88A G
stringValue88H S
,88S T
string88U [
message88\ c
)88c d
{99 	
Regex:: 
regex:: 
=:: 
new:: 
Regex:: #
(::# $
pattern::$ +
)::+ ,
;::, -
if<< 
(<< 
!<< 
regex<< 
.<< 
IsMatch<< 
(<< 
stringValue<< *
)<<* +
)<<+ ,
{== 
throw>> 
new>> %
InvalidOperationException>> 3
(>>3 4
message>>4 ;
)>>; <
;>>< =
}?? 
}@@ 	
publicBB 
staticBB 
voidBB "
AssertArgumentNotEmptyBB 1
(BB1 2
stringBB2 8
stringValueBB9 D
,BBD E
stringBBF L
messageBBM T
)BBT U
{CC 	
ifDD 
(DD 
stringValueDD 
==DD 
nullDD #
||DD$ &
stringValueDD' 2
.DD2 3
TrimDD3 7
(DD7 8
)DD8 9
.DD9 :
LengthDD: @
==DDA C
$numDDD E
)DDE F
{EE 
throwFF 
newFF %
InvalidOperationExceptionFF 3
(FF3 4
messageFF4 ;
)FF; <
;FF< =
}GG 
}HH 	
publicJJ 
staticJJ 
voidJJ #
AssertArgumentNotEqualsJJ 2
(JJ2 3
objectJJ3 9
object1JJ: A
,JJA B
objectJJC I
object2JJJ Q
,JJQ R
stringJJS Y
messageJJZ a
)JJa b
{KK 	
ifLL 
(LL 
object1LL 
.LL 
EqualsLL 
(LL 
object2LL &
)LL& '
)LL' (
{MM 
throwNN 
newNN %
InvalidOperationExceptionNN 3
(NN3 4
messageNN4 ;
)NN; <
;NN< =
}OO 
}PP 	
publicRR 
staticRR 
voidRR !
AssertArgumentNotNullRR 0
(RR0 1
objectRR1 7
object1RR8 ?
,RR? @
stringRRA G
messageRRH O
)RRO P
{SS 	
ifTT 
(TT 
object1TT 
==TT 
nullTT 
)TT  
{UU 
throwVV 
newVV %
InvalidOperationExceptionVV 3
(VV3 4
messageVV4 ;
)VV; <
;VV< =
}WW 
}XX 	
publicZZ 
staticZZ 
voidZZ 
AssertArgumentRangeZZ .
(ZZ. /
doubleZZ/ 5
valueZZ6 ;
,ZZ; <
doubleZZ= C
minimumZZD K
,ZZK L
doubleZZM S
maximumZZT [
,ZZ[ \
stringZZ] c
messageZZd k
)ZZk l
{[[ 	
if\\ 
(\\ 
value\\ 
<\\ 
minimum\\ 
||\\  "
value\\# (
>\\) *
maximum\\+ 2
)\\2 3
{]] 
throw^^ 
new^^ %
InvalidOperationException^^ 3
(^^3 4
message^^4 ;
)^^; <
;^^< =
}__ 
}`` 	
publicbb 
staticbb 
voidbb 
AssertArgumentRangebb .
(bb. /
floatbb/ 4
valuebb5 :
,bb: ;
floatbb< A
minimumbbB I
,bbI J
floatbbK P
maximumbbQ X
,bbX Y
stringbbZ `
messagebba h
)bbh i
{cc 	
ifdd 
(dd 
valuedd 
<dd 
minimumdd 
||dd  "
valuedd# (
>dd) *
maximumdd+ 2
)dd2 3
{ee 
throwff 
newff %
InvalidOperationExceptionff 3
(ff3 4
messageff4 ;
)ff; <
;ff< =
}gg 
}hh 	
publicjj 
staticjj 
voidjj 
AssertArgumentRangejj .
(jj. /
intjj/ 2
valuejj3 8
,jj8 9
intjj: =
minimumjj> E
,jjE F
intjjG J
maximumjjK R
,jjR S
stringjjT Z
messagejj[ b
)jjb c
{kk 	
ifll 
(ll 
valuell 
<ll 
minimumll 
||ll  "
valuell# (
>ll) *
maximumll+ 2
)ll2 3
{mm 
thrownn 
newnn %
InvalidOperationExceptionnn 3
(nn3 4
messagenn4 ;
)nn; <
;nn< =
}oo 
}pp 	
publicrr 
staticrr 
voidrr 
AssertArgumentRangerr .
(rr. /
longrr/ 3
valuerr4 9
,rr9 :
longrr; ?
minimumrr@ G
,rrG H
longrrI M
maximumrrN U
,rrU V
stringrrW ]
messagerr^ e
)rre f
{ss 	
iftt 
(tt 
valuett 
<tt 
minimumtt 
||tt  "
valuett# (
>tt) *
maximumtt+ 2
)tt2 3
{uu 
throwvv 
newvv %
InvalidOperationExceptionvv 3
(vv3 4
messagevv4 ;
)vv; <
;vv< =
}ww 
}xx 	
publiczz 
staticzz 
voidzz 
AssertArgumentTruezz -
(zz- .
boolzz. 2
	boolValuezz3 <
,zz< =
stringzz> D
messagezzE L
)zzL M
{{{ 	
if|| 
(|| 
!|| 
	boolValue|| 
)|| 
{}} 
throw~~ 
new~~ %
InvalidOperationException~~ 3
(~~3 4
message~~4 ;
)~~; <
;~~< =
} 
}
ÄÄ 	
public
ÇÇ 
static
ÇÇ 
void
ÇÇ 
AssertStateFalse
ÇÇ +
(
ÇÇ+ ,
bool
ÇÇ, 0
	boolValue
ÇÇ1 :
,
ÇÇ: ;
string
ÇÇ< B
message
ÇÇC J
)
ÇÇJ K
{
ÉÉ 	
if
ÑÑ 
(
ÑÑ 
	boolValue
ÑÑ 
)
ÑÑ 
{
ÖÖ 
throw
ÜÜ 
new
ÜÜ '
InvalidOperationException
ÜÜ 3
(
ÜÜ3 4
message
ÜÜ4 ;
)
ÜÜ; <
;
ÜÜ< =
}
áá 
}
àà 	
public
ää 
static
ää 
void
ää 
AssertStateTrue
ää *
(
ää* +
bool
ää+ /
	boolValue
ää0 9
,
ää9 :
string
ää; A
message
ääB I
)
ääI J
{
ãã 	
if
åå 
(
åå 
!
åå 
	boolValue
åå 
)
åå 
{
çç 
throw
éé 
new
éé '
InvalidOperationException
éé 3
(
éé3 4
message
éé4 ;
)
éé; <
;
éé< =
}
èè 
}
êê 	
	protected
íí 
AssertionConcern
íí "
(
íí" #
)
íí# $
{
ìì 	
}
îî 	
	protected
ññ 
void
ññ &
SelfAssertArgumentEquals
ññ /
(
ññ/ 0
object
ññ0 6
object1
ññ7 >
,
ññ> ?
object
ññ@ F
object2
ññG N
,
ññN O
string
ññP V
message
ññW ^
)
ññ^ _
{
óó 	
AssertionConcern
òò 
.
òò "
AssertArgumentEquals
òò 1
(
òò1 2
object1
òò2 9
,
òò9 :
object2
òò; B
,
òòB C
message
òòD K
)
òòK L
;
òòL M
}
ôô 	
	protected
õõ 
void
õõ %
SelfAssertArgumentFalse
õõ .
(
õõ. /
bool
õõ/ 3
	boolValue
õõ4 =
,
õõ= >
string
õõ? E
message
õõF M
)
õõM N
{
úú 	
AssertionConcern
ùù 
.
ùù !
AssertArgumentFalse
ùù 0
(
ùù0 1
	boolValue
ùù1 :
,
ùù: ;
message
ùù< C
)
ùùC D
;
ùùD E
}
ûû 	
	protected
†† 
void
†† &
SelfAssertArgumentLength
†† /
(
††/ 0
string
††0 6
stringValue
††7 B
,
††B C
int
††D G
maximum
††H O
,
††O P
string
††Q W
message
††X _
)
††_ `
{
°° 	
AssertionConcern
¢¢ 
.
¢¢ "
AssertArgumentLength
¢¢ 1
(
¢¢1 2
stringValue
¢¢2 =
,
¢¢= >
maximum
¢¢? F
,
¢¢F G
message
¢¢H O
)
¢¢O P
;
¢¢P Q
}
££ 	
	protected
•• 
void
•• &
SelfAssertArgumentLength
•• /
(
••/ 0
string
••0 6
stringValue
••7 B
,
••B C
int
••D G
minimum
••H O
,
••O P
int
••Q T
maximum
••U \
,
••\ ]
string
••^ d
message
••e l
)
••l m
{
¶¶ 	
AssertionConcern
ßß 
.
ßß "
AssertArgumentLength
ßß 1
(
ßß1 2
stringValue
ßß2 =
,
ßß= >
minimum
ßß? F
,
ßßF G
maximum
ßßH O
,
ßßO P
message
ßßQ X
)
ßßX Y
;
ßßY Z
}
®® 	
	protected
™™ 
void
™™ '
SelfAssertArgumentMatches
™™ 0
(
™™0 1
string
™™1 7
pattern
™™8 ?
,
™™? @
string
™™A G
stringValue
™™H S
,
™™S T
string
™™U [
message
™™\ c
)
™™c d
{
´´ 	
AssertionConcern
¨¨ 
.
¨¨ #
AssertArgumentMatches
¨¨ 2
(
¨¨2 3
pattern
¨¨3 :
,
¨¨: ;
stringValue
¨¨< G
,
¨¨G H
message
¨¨I P
)
¨¨P Q
;
¨¨Q R
}
≠≠ 	
	protected
ØØ 
void
ØØ (
SelfAssertArgumentNotEmpty
ØØ 1
(
ØØ1 2
string
ØØ2 8
stringValue
ØØ9 D
,
ØØD E
string
ØØF L
message
ØØM T
)
ØØT U
{
∞∞ 	
AssertionConcern
±± 
.
±± $
AssertArgumentNotEmpty
±± 3
(
±±3 4
stringValue
±±4 ?
,
±±? @
message
±±A H
)
±±H I
;
±±I J
}
≤≤ 	
	protected
¥¥ 
void
¥¥ )
SelfAssertArgumentNotEquals
¥¥ 2
(
¥¥2 3
object
¥¥3 9
object1
¥¥: A
,
¥¥A B
object
¥¥C I
object2
¥¥J Q
,
¥¥Q R
string
¥¥S Y
message
¥¥Z a
)
¥¥a b
{
µµ 	
AssertionConcern
∂∂ 
.
∂∂ %
AssertArgumentNotEquals
∂∂ 4
(
∂∂4 5
object1
∂∂5 <
,
∂∂< =
object2
∂∂> E
,
∂∂E F
message
∂∂G N
)
∂∂N O
;
∂∂O P
}
∑∑ 	
	protected
ππ 
void
ππ '
SelfAssertArgumentNotNull
ππ 0
(
ππ0 1
object
ππ1 7
object1
ππ8 ?
,
ππ? @
string
ππA G
message
ππH O
)
ππO P
{
∫∫ 	
AssertionConcern
ªª 
.
ªª #
AssertArgumentNotNull
ªª 2
(
ªª2 3
object1
ªª3 :
,
ªª: ;
message
ªª< C
)
ªªC D
;
ªªD E
}
ºº 	
	protected
ææ 
void
ææ %
SelfAssertArgumentRange
ææ .
(
ææ. /
double
ææ/ 5
value
ææ6 ;
,
ææ; <
double
ææ= C
minimum
ææD K
,
ææK L
double
ææM S
maximum
ææT [
,
ææ[ \
string
ææ] c
message
ææd k
)
ææk l
{
øø 	
AssertionConcern
¿¿ 
.
¿¿ !
AssertArgumentRange
¿¿ 0
(
¿¿0 1
value
¿¿1 6
,
¿¿6 7
minimum
¿¿8 ?
,
¿¿? @
maximum
¿¿A H
,
¿¿H I
message
¿¿J Q
)
¿¿Q R
;
¿¿R S
}
¡¡ 	
	protected
√√ 
void
√√ %
SelfAssertArgumentRange
√√ .
(
√√. /
float
√√/ 4
value
√√5 :
,
√√: ;
float
√√< A
minimum
√√B I
,
√√I J
float
√√K P
maximum
√√Q X
,
√√X Y
string
√√Z `
message
√√a h
)
√√h i
{
ƒƒ 	
AssertionConcern
≈≈ 
.
≈≈ !
AssertArgumentRange
≈≈ 0
(
≈≈0 1
value
≈≈1 6
,
≈≈6 7
minimum
≈≈8 ?
,
≈≈? @
maximum
≈≈A H
,
≈≈H I
message
≈≈J Q
)
≈≈Q R
;
≈≈R S
}
∆∆ 	
	protected
»» 
void
»» %
SelfAssertArgumentRange
»» .
(
»». /
int
»»/ 2
value
»»3 8
,
»»8 9
int
»»: =
minimum
»»> E
,
»»E F
int
»»G J
maximum
»»K R
,
»»R S
string
»»T Z
message
»»[ b
)
»»b c
{
…… 	
AssertionConcern
   
.
   !
AssertArgumentRange
   0
(
  0 1
value
  1 6
,
  6 7
minimum
  8 ?
,
  ? @
maximum
  A H
,
  H I
message
  J Q
)
  Q R
;
  R S
}
ÀÀ 	
	protected
ÕÕ 
void
ÕÕ %
SelfAssertArgumentRange
ÕÕ .
(
ÕÕ. /
long
ÕÕ/ 3
value
ÕÕ4 9
,
ÕÕ9 :
long
ÕÕ; ?
minimum
ÕÕ@ G
,
ÕÕG H
long
ÕÕI M
maximum
ÕÕN U
,
ÕÕU V
string
ÕÕW ]
message
ÕÕ^ e
)
ÕÕe f
{
ŒŒ 	
AssertionConcern
œœ 
.
œœ !
AssertArgumentRange
œœ 0
(
œœ0 1
value
œœ1 6
,
œœ6 7
minimum
œœ8 ?
,
œœ? @
maximum
œœA H
,
œœH I
message
œœJ Q
)
œœQ R
;
œœR S
}
–– 	
	protected
““ 
void
““ $
SelfAssertArgumentTrue
““ -
(
““- .
bool
““. 2
	boolValue
““3 <
,
““< =
string
““> D
message
““E L
)
““L M
{
”” 	
AssertionConcern
‘‘ 
.
‘‘  
AssertArgumentTrue
‘‘ /
(
‘‘/ 0
	boolValue
‘‘0 9
,
‘‘9 :
message
‘‘; B
)
‘‘B C
;
‘‘C D
}
’’ 	
	protected
◊◊ 
void
◊◊ "
SelfAssertStateFalse
◊◊ +
(
◊◊+ ,
bool
◊◊, 0
	boolValue
◊◊1 :
,
◊◊: ;
string
◊◊< B
message
◊◊C J
)
◊◊J K
{
ÿÿ 	
AssertionConcern
ŸŸ 
.
ŸŸ 
AssertStateFalse
ŸŸ -
(
ŸŸ- .
	boolValue
ŸŸ. 7
,
ŸŸ7 8
message
ŸŸ9 @
)
ŸŸ@ A
;
ŸŸA B
}
⁄⁄ 	
	protected
‹‹ 
void
‹‹ !
SelfAssertStateTrue
‹‹ *
(
‹‹* +
bool
‹‹+ /
	boolValue
‹‹0 9
,
‹‹9 :
string
‹‹; A
message
‹‹B I
)
‹‹I J
{
›› 	
AssertionConcern
ﬁﬁ 
.
ﬁﬁ 
AssertStateTrue
ﬁﬁ ,
(
ﬁﬁ, -
	boolValue
ﬁﬁ- 6
,
ﬁﬁ6 7
message
ﬁﬁ8 ?
)
ﬁﬁ? @
;
ﬁﬁ@ A
}
ﬂﬂ 	
}
‡‡ 
}·· ø
2D:\service-orders\src\DOMAIN\Base\AggregateRoot.cs
	namespace 	
DOMAIN
 
. 
Base 
; 
public 
class 
AggregateRoot 
{ 
[ 
Key 
] 	
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
=! "
Guid# '
.' (
NewGuid( /
(/ 0
)0 1
;1 2
public		 

DateTime		 
	CreatedAt		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
=		, -
DateTime		. 6
.		6 7
UtcNow		7 =
;		= >
public

 

DateTime

 
?

 
DeleteAt

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
} 