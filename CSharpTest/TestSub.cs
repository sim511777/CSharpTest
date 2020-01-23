using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CSharpTest {
    class TestSub {
        public static int LocalFunctionFactorial(int n) {
            return nthFactorial(n);

            int nthFactorial(int number) => (number < 2) ?
                1 : number * nthFactorial(number - 1);
        }

        public static int LambdaFactorial(int n) {
            Func<int, int> nthFactorial = default(Func<int, int>);

            nthFactorial = (number) => (number < 2) ?
                1 : number * nthFactorial(number - 1);

            return nthFactorial(n);
        }
        public static DialogResult InputBox(string title, string promptText, ref string value) {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }

    class SomeClass : IDisposable {
        public SomeClass() {
            Console.WriteLine($"SomeClass Constructed: {this.GetHashCode()}");
        }
        ~SomeClass() {
            Console.WriteLine($"SomeClass Finalized: {this.GetHashCode()}");
        }
        public void Dispose() {
            Console.WriteLine($"SomeClass Disposed: {this.GetHashCode()}");
        }
    }

    class Animal {
        protected string name;
        public Animal(string name) => this.name = name;
        public void Move() {
            Console.WriteLine($"{name} is moving");
        }
    }

    class Bird : Animal {
        public Bird(string name) : base(name) { }
        public void Fly() {
            Console.WriteLine($"{name} is flying");
        }
    }

    class Duck : Bird {
        public Duck(string name) : base(name) { }
        public void Swim() {
            Console.WriteLine($"{name} is swimming");
        }
    }

    class Glb {
        public static void quicksort(int[] A, int lo, int hi) {
            if (lo < hi) {
                int p = partition(A, lo, hi);
                quicksort(A, lo, p);
                quicksort(A, p + 1, hi);
            }
        }

        public static int partition(int[] A, int lo, int hi) {
            Console.Write("part: " + new string(' ', lo * 4));
            Console.WriteLine(string.Join("", A.Skip(lo).Take(hi - lo + 1).Select((n, idx) => idx + lo == lo ? $"#{n,2}#" : $" {n,2} ")));
            int pivot = A[lo];  // 첫번째 요소값을 피벗으로
            int i = lo;     // 첫번째 요소 부터 조회
            int j = hi;     // 마지막 요소 부터 조회
            while (true) {
                while (A[i] < pivot) i++;   // 하나씩 뺌
                while (A[j] > pivot) j--;   // 하나씩 뺌
                if (i >= j) return j;       // 겹치거나 바뀌었다면 겹치거나 작은 값 리턴
                Console.Write("swap: " + new string(' ', lo * 4));
                Console.WriteLine(string.Join("", A.Skip(lo).Take(hi - lo + 1).Select((n, idx) => idx + lo == i || idx + lo == j ? $"[{n,2}]" : $" {n,2} ")));
                swap(ref A[i], ref A[j]);   // 스왑
            }
        }

        public static void swap(ref int a, ref int b) {
            int temp = a;
            a = b;
            b = temp;
        }

        public static string ToHexString(byte[] bytes) {
            var strings = bytes.Select(b => b.ToString("x2")).ToArray();
            var hexString = string.Join("-", strings);
            return hexString;
        }

        public static string ToBinaryString(byte[] bytes) {
            var strings = bytes.Select(b => Convert.ToString( b, 2 ).PadLeft( 8, '0' )).ToArray();
            var hexString = string.Join("-", strings);
            return hexString;
        }
    }

    enum ArrayStyle { Random, AscSorted, DescSorted }

    public class Super {
        public void NormalPrint() {
            Console.WriteLine("Super.NormalPrint()");
        }
        public virtual void OverridePrint() {
            Console.WriteLine("Super.OverridePrint()");
        }
    }

    public class Sub : Super {
        new private void NormalPrint() {
            Console.WriteLine("Sub.NormalPrint()");
        }
        public override void OverridePrint() {
            Console.WriteLine("Sub.OverridePrint()");
        }
    }

    public class FobonacciClass {
        // 일반 함수
        public static int FibReq(int n) {
            return n < 2 ? n : FibReq(n - 1) + FibReq(n - 2);
        }

        // 식 본문 함수
        public static int FibExpBody(int n) => n < 2 ? n : FibExpBody(n - 1) + FibExpBody(n - 2);

        // 람다식 대리자
        public static Func<int, int> FibLambda = n => n < 2 ? n : FibLambda(n - 1) + FibLambda(n - 2);

        // 일반 함수
        public static Dictionary<int, int> dicMemo = new Dictionary<int, int>();
        public static int FibMemo(int n) {
            if (dicMemo.ContainsKey(n))
                return dicMemo[n];
            var result = n < 2 ? n : FibMemo(n - 1) + FibMemo(n - 2);
            dicMemo.Add(n, result);
            return result;
        }

        // 람다식 대리자
        public static Func<int, int> FibMemoGeneric = Memoize((int n) => {
            return n < 2 ? n : FibMemoGeneric(n - 1) + FibMemoGeneric(n - 2);
        });

        // 메모이제이션 함수
        public static Func<A, R> Memoize<A, R>(Func<A, R> f) {
            var map = new Dictionary<A, R>();
            return a => {
                R value;
                if (map.TryGetValue(a, out value) == false)
                    map[a] = f(a);
                return map[a];
            };
        }
    }

    public class ClassPoint {
        public int X;
        public int Y;
        public ClassPoint() {
            X = 0;
            Y = 0;
        }
    }

    public class Crc32 {
        private const uint CRC_TSIZE = 256U;
        private const uint CRC32_POLYNOMIAL = 0x04C11DB7U;
        private const uint CRC32_INIT = 0xFFFFFFFFU;

        private static readonly uint[] dwCRCTable = new uint[CRC_TSIZE];

        // CRC테이블 생성
        static Crc32() {
            uint CRC = 0;
            for (ushort wIndex = 0; wIndex < CRC_TSIZE; wIndex++) {
                CRC = wIndex;
                for (ushort wSize = 0; wSize < 8; wSize++) {
                    if ((CRC & 1) != 0U) {
                        CRC >>= 1;
                        CRC ^= CRC32_POLYNOMIAL;
                    } else {
                        CRC >>= 1;
                    }
                }
                dwCRCTable[wIndex] = CRC;
            }
        }

        public static uint GetCRCT(byte[] pData, int iPointFrom, int iPointTo) {
            uint CRC = CRC32_INIT;
            byte Index = 0;

            for (int i = iPointFrom; i < iPointTo; i++) {
                Index = (byte)(pData[i] ^ CRC);
                CRC >>= 8;
                CRC ^= dwCRCTable[Index];
            }

            return CRC;
        }

        public static uint GetCRCT(byte[] pData) {
            return GetCRCT(pData, 0, pData.Length);
        }
    }

    public static class EnumerableShim {
        public static IEnumerable<TSource> RandomShuffle<TSource>(this IEnumerable<TSource> source) {
            return RandomShuffle(source, source.Count());
        }
        
        public static IEnumerable<TSource> RandomShuffle<TSource>(this IEnumerable<TSource> source, int count) {
            var array = source.ToArray();
            var rnd = new Random();
            for (int i = 0; i < count; i++) {
                int j = rnd.Next(i, array.Length);
                yield return array[j];
                array[j] = array[i];
            }
        }
    }

    public static class Voca {
        public static string GetPronoun(string word) {
            try {
                string url = $"http://aha-dic.com/View.asp?word={word}";
                using (var client = new WebClient()) {
                    client.Encoding = Encoding.UTF8;
                    var html = client.DownloadString(url);
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    var nodes = doc.DocumentNode.SelectNodes("//div[@class='phonetic']");
                    var texts = nodes.ElementAt(0).ChildNodes.Where(cn => cn.NodeType == HtmlNodeType.Text).Select(cn => cn.InnerText.Trim()).Where(text => text != string.Empty);
                    return string.Join(" / ", texts);
                }
            } catch (Exception ex) {
                return ex.Message;
            }
        }
        public static string words =
@"progress
propose
produce
protect
pros and cons
predict
precaution
premature
preview
forehead
forefather
foremost
foresee
postpone
postscript
income
indoor
infect
insight
invest
outcome
outline
outlook
outstanding
outlet
outdo
utter
utmost
overcome
overlook
overseas
overhead
overtake
overwhelm
overthrow
overwork
extracurricular
extraordinary
extraterrestrial
extrovert
recover
recycle
replace
reproduce
revive
remain
remove
represent
international
interpret
interfere
interaction
dialog(ue)
dialect
diameter
transfer
transform
translate
transplant
depress
despise
depart
detect
descend
detach
declare
debate
undergraduate
undergo
undertake
exhale
expand
explicit
expose
exaggerate
exchange
exhaust
extinct
exceed
ex-convict
uneasy
unfair
unfortunate
unlikely
unusual
unlock
disagree
disappear
discourage
disorder
dismiss
dispose
display
differ
independent
inevitable
inexpensive
illegal
immoral
irrelevant
antibiotic
antibody
antarctic
antonym
contrast
contrary
counterattack
counterfeit
counterpart
controversy
withdraw
withhold
withstand
separate
secure
select
segregate
superior
superb
superficial
sovereign
surface
uphold
upright
upset
upside
submarine
subconscious
suffer
suggest
support
suppress
anticipate
antique
advantage
ancestor
ancient
advance
company
compile
compose
compromise
conform
confront
correspond
confirm
condense
sympathy
symphony
synchronize
synthetic
multimedia
multiple
multitude
absorb
abnormal
absolute
absurd
enable
enforce
enlarge
enrich
entitle
enclose
autobiography
autograph
automobile
telepathy
telescope
perfect
permanent
persevere
persist
persuade
perfume
perspective
adjust
accompany
account
accumulate
accuse
accustomed
appoint
approach
abandon
await
arrogant
aboard
abroad
alike
arise
arouse
amaze
ashamed
geography
geology
geometry
monarch
monolog(ue)
union
unique
unify
unity
universe
bicycle
bilingual
duplicate
twilight
twist
triangle
tribe
triple
trivial
hemisphere
semi-annual
semi-final
quadrangle
quarter
quartet
pentagon
pentathlon
hexagon
September
October
octopus
November
decade
December
decimal
centigrade
centimeter
century
kilometer
million
millionaire
movable
reliable
sensible
careful
hopeful
powerful
countless
endless
eastern
western
southern
northern
basic
typical
familiar
supplementary
satisfactory
costly
friendly
famous
dangerous
fortunate
favorite
pleasant
different
foolish
selfish
national
spiritual
active
effective
healthy
lucky
childish
childlike
comparable
comparative
considerable
considerate
continual
continuous
economic
economical
historic
historical
industrial
industrious
intellectual
intelligent
respectable
respectful
sensible
sensitive
social
sociable
literal
literary
literate
successful
successive
imaginable
imaginative
imaginary
speaker
interviewer
employer
interviewee
employee
inventor
prosecutor
assistant
attendant
servant
resident
artist
novelist
relative
representative
secretary
missionary
economics
politics
physics
logic
appearance
difference
certainty
ability
discovery
bravery
arrival
proposal
creature
failure
agreement
assessment
movement
translation
introduction
invitation
truth
width
kindness
weakness
accuracy
privacy
citizenship
leadership
childhood
neighborhood
realism
socialism
criticism
booklet
leaflet
cigarette
realize
civilize
originate
motivate
justify
simplify
deepen
frighten
loudly
comfortably
eastward
upward
always
anyway
otherwise
agent
agony
active
actual
react
navigate
altitude
abolish
adolescent
adult
alter
alternative
alien
anguish
anxious
anniversary
annual
apt
adapt
attitude
artificial
artisan
artist
disaster
astronaut
astronomy
astrology
consider
audience
obey
bandage
bond
bundle
bind
barrier
bar
embarrass
biography
biology
capital
capable
capture
chief
achieve
occupy
participate
accept
except
deceive
receive
perceive
conceive
conceit
career
carpenter
carriage
carrier
charge
discharge
cast
broadcast
forecast
precede
exceed
proceed
succeed
predecessor
incessant
access
cease
concentrate
eccentric
concern
discern
discriminate
crisis
criticize
certain
certificate
accident
incident
casual
occasion
decay
suicide
decision
precise
circular
circulate
circumstance
circuit
cite
recite
excite
civil
civilize
citizen
claim
exclaim
proclaim
council
decline
inclined
climate
conclude
include
exclude
closet
disclose
recognize
diagnose
ignore
noble
acknowledge
acquaint
cordial
discord
accord
courage
encourage
core
corporate
corps
create
recreate
increase
decrease
concrete
recruit
credit
incredible
creed
cultivate
culture
agriculture
colony
current
curriculum
excursion
occur
cure
curious
accurate
damage
condemn
debt
due
duty
render
surrender
add
edition
rent
dictate
addict
contradict
dedicate
indicate
index
donation
anecdote
dose
conduct
educate
deduct
induce
introduce
reduce
electric
electronic
equal
equivalent
adequate
identify
essence
absent
present
estimate
overestimate
esteem
fable
fate
fame
infancy
preface
confess
profess
manufacture
facility
faculty
factor
factual
effect
affect
defect
fiction
proficient
sufficient
efficient
profit
benefit
qualify
satisfy
affair
false
fail
fault
fare
farewell
welfare
defend
offend
prefer
refer
confer
infer
indifferent
fertile
confident
faith
defy
final
finance
confine
refine
define
infinite
reflect
flexible
conflict
inflict
fluid
fluent
influence
influenza
form
inform
reform
formula
fort
effort
comfort
force
reinforce
fragment
fragile
fraction
fund
fundamental
profound
found
confuse
refuse
futile
refund
regard
garment
guard
guarantee
generate
generous
general
genetic
genius
gentle
genuine
ingenious
oxygen
hydrogen
pregnant
gesture
digest
register
gradual
graduate
aggressive
congress
degree
ingredient
graphic
paragraph
photograph
grammar
gratitude
congratulate
agree
grace
grave
aggravate
grieve
habit
inhabit
habitat
exhibit
prohibit
able
heredity
heritage
inherit
heir
host
hostile
hospitable
human
humble
humility
peninsula
isolate
exit
initial
initiate
transit
perish
issue
inject
object
subject
project
reject
just
justify
judge
prejudice
injure
labor
laboratory
elaborate
relate
legislate
relax
analyze
paralyze
release
delay
relay
collect
recollect
elect
neglect
intellect
lecture
elegant
legend
diligence
legal
privilege
delegate
legacy
colleague
loyal
elevate
relevant
relieve
liberal
liberate
deliver
oblige
religion
ally
rally
liable
eliminate
preliminary
limit
literal
literate
literature
literary
local
locate
allocate
logic
apology
prolog(ue)
ecology
psychology
long
belong
prolong
length
linger
magnitude
magnify
master
masterpiece
major
mayor
majestic
maximum
demand
command
recommend
manual
manuscript
maintain
manage
manipulate
mechanism
mechanic
machinery
medium
medi(a)eval
mediate
Mediterranean
immediate
intermediate
mean
meanwhile
memory
remember
mental
mention
comment
monument
monitor
summon
remind
commerce
merchant
mercy
thermometer
measure
dimension
immense
immigrate
emigrate
minor
minister
administer
diminish
eminent
imminent
prominent
minimum
miracle
admire
marvel(l)ous
admit
commit
committee
emit
omit
permit
submit
transmit
missile
mission
commission
mess
moderate
modernize
modest
modify
accommodate
commodity
mold
mortal
mortgage
murder
move
motive
emotion
promote
remote
moment
mob
community
communism
communicate
common
mutual
commute
native
nation
nature
negative
deny
neutral
norm
enormous
note
notice
notify
notion
announce
pronounce
novel
innovate
renew
nutrition
nourish
nurse
melody
comedy
tragedy
operate
cooperate
option
adopt
opinion
ordinary
subordinate
origin
orient
prepare
apparatus
emperor
imperial
apparent
transparent
appear
compare
peer
partial
partly
particle
particular
apart
portion
proportion
passage
passenger
passerby
pastime
surpass
pace
pathetic
patient
passion
compassion
patriot
patron
pattern
pedestrian
expedition
compel
expel
appeal
impulse
penalty
painful
punish
depend
suspend
compensate
pension
ponder
experience
experiment
expert
peril
compete
competence
petition
appetite
repeat
phantom
phenomenon
fantasy
fancy
pant
emphasize
phase
applaud
explode
complete
complement
implement
compliment
accomplish
supply
plenty
please
plead
complicate
simplicity
complex
perplex
imply
apply
diplomatic
employ
exploit
explore
deplore
political
policy
metropolis
popular
populate
public
publish
republic
export
import
transport
portable
opportunity
pose
positive
deposit
impose
purpose
oppose
suppose
component
opponent
compound
possible
possess
potential
precious
appreciate
praise
priceless
comprehend
prison
imprison
enterprise
surprising
prey
pressure
express
impress
oppress
prime
primary
primitive
principal
principle
prior
private
deprive
probable
probe
prove
approve
proper
property
appropriate
punctual
punctuate
disappoint
dispute
reputation
acquire
require
inquire
conquer
request
exquisite
range
arrange
rank
rate
rational
reason
erect
correct
direct
region
regular
regulate
reign
royal
rotate
control
enrol(l)
bankrupt
corrupt
erupt
interrupt
route
routine
sacred
sacrifice
saint
ascend
scale
escalator
conscious
conscience
scientific
describe
prescribe
subscribe
ascribe
section
insect
segment
sense
sensation
nonsense
assent
sentiment
consent
resent
scent
sequence
consequence
subsequent
suit
pursue
execute
insert
desert
exert
series
conserve
deserve
preserve
observe
reserve
reside
president
session
settle
signature
signify
assign
resign
seal
similar
resemble
assemble
seemingly
simulation
social
associate
sociology
sole
solitary
solitude
desolate
solve
resolve
dissolve
philosophy
sophisticated
sophomore
spectacle
aspect
expect
inspect
respect
prospect
suspect
species
specific
special
specimen
despite
prosper
despair
sphere
atmosphere
hemisphere
spirit
aspire
expire
inspire
respond
sponsor
stand
standard
state
statistics
statue
status
stable
establish
constant
ecstasy
estate
instance
instant
obstacle
substance
assist
consist
exist
insist
resist
arrest
cost
steady
system
constitute
institute
substitute
destined
superstition
distinguish
extinguish
distinct
instinct
stimulate
strict
restrict
district
strain
restrain
strait
distress
stress
prestige
structure
construct
instruct
destroy
instrument
industry
insult
result
salmon
assume
consume
presume
resume
assure
insure
surge
source
resource
attach
attack
stake
tact
contact
integrate
attain
entire
tailor
retail
detail
contain
entertain
obtain
retain
sustain
content
continent
continue
technique
technology
temperate
temperature
temper
temperament
temporary
contemporary
tempt
attempt
attend
pretend
extend
intend
tend
tender
tense
intense
terminal
terminate
determine
term
terrible
terror
terrify
terrestrial
territory
testify
contest
protest
text
context
textile
theology
enthusiasm
tone
intonation
monotonous
tune
torture
distort
torment
abstract
attract
contract
distract
extract
treat
treaty
retreat
trace
track
trail
portray
tradition
betray
traitor
tremble
tremendous
attribute
contribute
distribute
trust
entrust
truthful
intrude
thrust
threat
disturb
trouble
abound
surround
urban
suburb
use
abuse
utensil
utilize
evade
invade
vague
extravagant
value
evaluate
valid
available
prevail
vanish
vain
avoid
vacuum
vacant
avenge
revenge
adventure
venture
convention
event
invent
prevent
avenue
convenient
intervene
souvenir
advertise
convert
vertical
converse
diverse
reverse
verse
version
divorce
via
obvious
previous
convey
voyage
victory
convict
convince
divide
individual
devise
widow
vigor
vegetable
vision
supervise
evidence
provide
view
interview
review
envy
survey
survive
vivid
vital
vocal
vocabulary
vocation
advocate
vowel
voluntary
benevolent
evolve
involve
revolve
volume
revolt
award
reward
aware
warn";
    }
}