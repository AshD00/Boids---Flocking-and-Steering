# Boids---Flocking-and-Steering

The following is the report that was attached to this project. It details the research that went into the project and the decisions made.

## Final report

#### Submitted for the BSc/BEng/MEng in

#### Computer Science with Games Development

#### April 20 22

#### by

## Ashok Cooper Johns Dalton

#### Word count: 7,418

## Abstract

This paper outlines and analyses the methodology behind simulating flocking and steering
behaviour in artificial intelligence. The goal of this project is to push an algorithm to its limits to
simulate a high quantity of 3D bird-like objects (boids), with the goal being a smooth frame rate
whilst supporting a large quantity of boids. The results will be achieved by breaking down the three
fundamental swarming principles and recreating them within an algorithm.

The paper then continues on to explain the decisions made throughout the project to reach its final
point, expanding upon the choices made in architecture and design. This includes breaking down
crucial decisions such as the development platform and the reasons for certain key choices.

Finally, the paper discusses future developments that could be made to expand upon the base
project, such as moving into multi-threading, if the project was ever improved.


ii

## Acknowledgements

I would like to thank Simon Grey for his continued supervision during this process, as well as Darren
McKie for his support over the last three years. I also wish to acknowledge the support of my mum,
dad, and the friends who have been with me during this process; Kisler JR Flores, Nichola Backhouse,
Chen Soon-Joo and the rest of the volleyball club for the great memories, Jake Preston for studying
with me throughout the last three years, and of course, Matthew McNeil, George Surridge and the
rest of our friends for always being there.


## Abstract i

- 1 Introduction Acknowledgements ii
   - 1.1 Background to the project
   - 1.2 Aims and objectives
- 2 Literature review
- 3 Requirements
   - 3.1 Product requirements
   - 3.2 Functional requirements
      - 3.2.1 Interfaces
      - 3.2.2 Functional Capabilities
      - 3.2.3 Performance Levels
      - 3.2.4 Data Structures/Elements
      - 3.2.5 Safety
      - 3.2.6 Reliability
      - 3.2.7 Constraints and Limitations
      - 3.2.8 Performance requirements
   - 3.3 Design constraints
- 4 Design
   - 4.1 Software design
   - 4.2 Experimental design
- 5 Implementation and testing
   - 5.1 Implementation
   - 5.2 Testing
- 6 Evaluation and discussion of results
   - 6.1 Objective 1: Create and implement a 2d swarming AI
   - 6.2 Objective 2: Move the AI into a 3d space
   - 6.3 Objective 3: Ensure the code is efficient
   - 6.4 Reflection
   - 6.5 Further work
- 7 Conclusion
- References
- Appendix A – Full set of data from the tests


## 1 Introduction

### 1.1 Background to the project

The idea for this project came about when reading about artificial intelligence and key concepts
within the industry. Initially interests lay in pathfinding, as this has been explored during earlier
modules at university. As I read into this topic, I happened across a video by popular youtuber
Sebastian Lague that discussed a coding concept called “Boids”. The video spent a great deal of time
showing off both the conceptual background to the topic, but also the code in action, and it was this
beautiful display of symmetry and fluidity from the entities that initially drew me to look further into
the project. Upon seeing it was one of the recommended projects, I took to it and submitted my
request. Beyond the scope of my initial interest in the project, I was also conscious of the fact that,
given boids is a known project within the Computer Science field, it would be a great portfolio piece
for future work prospects.

As for the background to the project itself, boids have been used across visual animated media,
from video games to television. Some examples include the birds sighted at the end of the first ‘Half
Life’ game, the class of which was aptly named ‘boid.’ Reynolds used the 1992 Tim Burton movie
Batman Returns in a reflective discussion of the topic, discussing how it utilised boids for the bat
swarm sighted in the skies of Gotham and the flocks of penguins that marched the streets (Reynolds,
1995). Most commonly however they make their appearances in video games such as the 2014
game Subnautica, which features schools of fish swimming as atmosphere.

Expanding outside the entertainment field, swarming behaviours have been studied for use in
scientific fields. Predictions of migration (Gökçe and Sahin, 2009) and behavioural models of species
that are recorded for their swarming behaviours can make use of flocking algorithms in their studies.

### 1.2 Aims and objectives

The primary goal of the project was to explore the limitations of code structure and design when
programming boids, focusing specifically on the efficiency and limit testing of both the code
structure as previously mentioned, but also to a degree Unity, as this was the chosen engine to
program in.

These objectives were achieved to varying degrees of success, and are listed below:

**Objective 1:** Create and implement a 2d swarming AI

- Create a model to visualise the boids
- Create the unity program
- Implement the three rules of flocking

This objective signified the start of the project. Whilst the objective title specifies a 2d environment,
the core milestones of the objective surround the building of the class itself. The reason that a 2d
environment was specified and heavily leant on was due to the fact that it was believed to be a
simpler start to the project than moving straight into 3d. The success of this could be marked by
tracking the boids to see if they do follow the three rules properly, which is only possible if the
model is displayable and easily recognisable in a mass.


**Objective 2:** Move the AI into a 3d space

- Include 3d axis in the boid class
- Add avoidance of physical space (for example, the walls of the game scene)
- Update the graphics of the scene to reflect a deep-sea appearance.

This objective marked the end of the visual changes, as well as the move from a two-dimensional
program to three. This would set the stage for how the final project would appear and, in many
ways, feel, although the background algorithm still had changes to be made. This objective did not
mark a technical achievement in terms of code, but instead reflected a mark point in the progress of
the code, as the visual product would reach its final point. This objective can be marked as complete
once the boids are able to successfully traverse a three-dimensional space.

**Objective 3:** Ensure the code is efficient

Whilst vague, this objective referred to all background changes that would need to be made to
ensure that the code remains efficient. This is one of the most important objectives of the project, as
the goal is to make an efficient piece of software. The achievable parts of this goal involve removing
cluttered methods and stream lining the run cycle to increase the number of boids that can be run at
once. This will be measured based on the performance of the software during varying runs of the
program.


## 2 Literature review

Flocking and Steering algorithms, or Boids, was created by Craig W. Reynolds in his paper _Flocks,
Herds, and Schools: A Distributed Behavioral Model_ (Reynolds, 1987). This paper breaks down the
concept of simulating the behaviour of a flock of bird-like (bird-oid) creatures in computer
animation. The model of Boids flocking he describes breaks down the behaviour into three key
concepts; Collision Avoidance, Velocity Matching and Flock Centering, which, in a later reflective
piece (Reynolds, 1995), he simplifies to Separation, Alignment and Cohesion.

**Separation** refers to the behaviour of maintaining a distance from neighbouring boids such that
they never overlap or collide. This is achieved by comparing the position of neighbouring boids to its
own position and making sure the distance does not fall below a threshold, which can be achieved
by steering away from local flock mates. Figure 1 below (Personal Creation inspired by the diagrams
drawn by Craig Reynolds) serves as a visual representation of this behaviour. The red lines are a
measure of distance between neighbouring agents within the scope of the boids check, and the red
arrow is the direction the boid will move towards to account for this.

```
Fig 1: Separation behaviour – Personal Creation
```
**Alignment** refers to the behaviour that drives boids to steer towards the “average heading of local
flockmates” (Reynolds, 1995). Simply put, this means the boid agents will travel in the same
direction as their local flock. This is achieved by checking the direction that nearby agents are
traveling in and aligning themselves with that heading. Figure 2 (Personal Creation) serves as a visual
representation of this behaviour. The green line refers to the boids current heading, blue lines are
the flock heading, and the red arrow is the direction the boid will turn towards.


```
Fig 2: Alignment behaviour – Personal Creation
```
**Cohesion** is the final behaviour detailed by Reynolds. This refers to agents steering towards an
average position of their local ‘flock’. By comparing the position of all local members, they can
create a central point and move towards it, thereby creating a flocking movement where the boids
group together. Figure 3 (Personal Creation) is a visual representation of this behaviour.

```
Fig 3: Cohesion behaviour – Personal Creation
```
When all three of the described behaviours are implemented at once it causes an emergence of
flocking behaviours, where in boid agents will maintain constantly stick close to their flock without
ever getting too close that they collide. Each individual behaviour can also be given a ‘weight’ which
decides how favoured each behaviour will be. A higher weighting on alignment for instance will
ensure that the boids place being within a flock over avoiding colliding, which could lead to overlap.
Therefore, finding a balanced weighting is an important focus when designing the boid agent code.


Since its inception, the boid model of flocking has seen use in both the movie and video game
industries. In the background and update website posted by Reynolds, he points to the 1992 Tim
Burton film Batman Returns for using boids to simulate the swarming bats above the city of Gotham,
as well as the marching army of penguins that are seen moving through its streets (Reynolds, 1995).

Reynolds also highlighted a reflective piece written about the 1994 Disney movie The Lion King. In
Brian Tiemann's web archive, CGI supervisor Scott Johnston describes the process of using computer
simulation for the iconic wildebeest stampede scene; "That's where computer animation can
sometimes make a difference. A stampede of thousands of wildebeests would be too laborious to
create by hand but animators working with computers can figure out what the behaviour of the
animal is and replicate it.” By implementing a flocking algorithm, they were able to create a more
realistic depiction of a stampede to truly complete the scene. See Fig 4 below for a screenshot of the
stampede.

```
Figure 4: Screenshot of the Wildebeest stampede which utilises a modified Flocking algorithm –
Disney's ‘The Lion King’ (1994)
```
Outside of the movie industry, boids have seen use in video games such as Half Life. During the final
section of the game, when the player visits several locations, a flock of alien birds can be seen flying
in the sky in the background. The game files showed that these bird-like creatures were in fact
named ‘Boids’ and implemented a version of Reynolds’ model. Figure 5 below shows a screenshot of
a small flock from the planet Xen.

```
Figure 5: Screenshot of a flock of ‘Boids’ utilising a version of Reynolds’ model – Valve (1999). Half
Life: Opposing Force
```

Since its inception, Reynolds’ model has seen attempts at expansion from several places. Delgado-
Mata et al expanded the basic model to incorporate animal fear when simulating the behaviours of
wild animals in their 2007 study. It was changed further by Hartman and Benes (2006) who
implemented a complementary force they named the change of leadership, wherein there is a
chance that a boid becomes a leader and attempts to escape.


## 3 Requirements

### 3.1 Product requirements

A unity application which creates a flock of swarming artificial intelligence.

The initial proposal for the project detailed a linear progression of development from 2d to 3d
boids, as during the research it had been concluded that it would be easier to migrate from working
in 2d to 3d. What was not accounted for is how unity handles these transitions, and the necessary
requirement of having separate 2d and 3d projects. As such, it became a question of why the 2d
code was being developed, as it was initially proposed as a way to ease progress. If it proved to be
no easier than the 3d algorithms, then why bother wasting time on a product that would not have
been shipped at the end of the project. Therefore, it was decided after some work to abandon a 2d
boids project, and focus on the product that would be shipped at the end (being the 3d scene).

As discussed in the literature review, an important aspect of boids is that their capacity to flock
varies dependent on the weighting of the various behaviours. Therefore, a level of adaptability is
required so that the program can fluidly change based on the requirements in each individual run of
the software.

The software’s primary audience would be those interested in the capabilities of the software
developer. As the software utilises a well-known algorithm model, being able to showcase a self-
made example of this will be a good portfolio piece for showing prospective employers.

Outside of this, the software also has the capacity to be implemented within a video game
environment. As discussed during the literature review section, the algorithm explored here has
seen use in a multitude of games, from action shooters like Half-Life to survival exploration games
like Subnautica. Therefore, in the future this algorithm could be added into games that could benefit
from birds or fish, or perhaps a game focused around flocking and hordes of entities.

### 3.2 Functional requirements

As described in the product requirements, the finished product needs to produce a 3d program with
a varying number of independent agents swarming according to Reynolds’ Boids model. The model’s
inherent modularity means that the program will need to reflect this with its own modularity and
adaptability in a case-by-case scenario, as described previously in the Product requirements section
of the report. Utilising unity script components allows the code to be changed without having to
ever open the scripts themselves as can be seen in figure 6 below.

```
Figure 6: Screenshot of the unity user interface for the script component. The weighting for each
behaviour can be seen, as well as the size of the space the boids can traverse and the properties
(quantity, speed, script) of the boids themselves. This will be discussed more in the next heading.
```

#### 3.2.1 Interfaces

The requirements for an interface in this project were fairly relaxed. The target audience of this
interface would be people who understand the project and are merely looking to run the simulation.
Whilst the play button is a simple to understand aspect of the interface, the ‘settings’ (behaviour
weighting) is intended for those that know what each value alters and how this could theoretically
change the way the software runs. This allows users to experiment to find the optimal weightings for
a perfect flock.

Therefore, users should be able to edit the weightings with ease, without having to have any in-
depth knowledge of the code architecture. Making sure this is simple is the key for a successful
implementation of the user interface.

```
Figure 7: Default unity view of the user interface (program not running). Along the righthand side is
the component interface that allows the changing of values. The centre is the display where the
scene will play when running.
```
#### 3.2.2 Functional Capabilities

The primary functionality of the software is to implement Reynolds’ model for flocking and assign it
to a collection of entities that then follow the three defined behaviours within a 3d space. This needs
to be achieved whilst maintaining a stable framerate.

As previously discussed, the users should also be allowed a degree of control over the system by
implementing control of the modular aspects of the code, be it the behaviours, boids or anything
else. This would allow the user control over how the flocks occur and many of the factors that
influence the flock. This also means the code can be reapplied in other projects, such as within video
games that may need flock algorithms.

#### 3.2.3 Performance Levels

Due to the high density of swarms, code performance is a major factor in flocking algorithms. In this
project the efficiency was the primary focus, so this factor was taken into much greater


consideration during programming. As there are three behaviours that each boid must take into
account when calculating their paths, it can fluctuate wildly if efficiency is not maximised.

For the purposes of measurement, we must set a baseline for an ‘adequate’ performance.
Therefore, it has been decided that the two benchmarks we will measure in this report are the
number of boids sustainable at 30 fps (frames per second) and the number of boids sustainable at 60
fps. This will give us two reference points as to how the performance fluctuates, as well as hitting
the two frame rate goals that are considered standard within gaming industries (60 being the
standard for games released on PC, whilst consoles such as the Nintendo Switch aim for 30).

#### 3.2.4 Data Structures/Elements

The most important data structure within the project is the list that holds all Boid members. There
has been a discussion surrounding the use of Quadtree structures as a secondary data structure to
improve the efficiency of the algorithm. This is discussed later in the report; however, the idea was
eventually pushed back due to time constraints.

#### 3.2.5 Safety

Whilst this project poses little physical risk to the user’s safety or wellbeing, it is worth considering
that, as the goal of this project is to push the architecture of the flocking model, there are risks
posed to the health of computer components running the software. Whilst this should not be a
major problem, it is worth the user taking consideration when running the software at higher values
if their CPU is not strong enough to sustain it. If they do not have a system that can support the
intensity, they may need to adjust the scope of the project (for example, reducing the number of
boids) to accommodate their system.

#### 3.2.6 Reliability

It should be worth considering that the project will only be tested on one system. Whilst the results
here are an accurate reflection of performance on a high-powered computer system, it is also worth
remembering that the same results likely would not be replicable on a weaker computer. Therefore,
whilst the results discussed in this report are accurate to the degree that they were tested, it is
worth considering that this is not indicative of all results that might be obtained.

#### 3.2.7 Constraints and Limitations

The primary constraint on the performance of the algorithm was definitely the lack of
multithreading. Even though the code was maxing out a single core of the CPU, due to the lack of
multithreading this work load was not spread evenly throughout the processor, which could have
severely lessened the load. If this project were to be approached in the future multithreading would
definitely be the first avenue to be pursued. The reason this was not within the scope of the project
is that parallel processing and multi-threading are skills that have not been explored enough by the
project leader to be implemented here.

#### 3.2.8 Performance requirements

A successful project will support at least 400 agents with a minimum of 30 FPS. This means that,
when running, there will be at least 400 boids in the scene, all of which using the flocking algorithm
that has been designed to follow all three established rules of flocking to an acceptable degree
whilst the program itself maintains an acceptable 30 frames per second.

The project will also take into consideration the number of boids maintainable whilst performing at
60 FPS, which, if the project were to be applied to a video games context in the future, is considered
the desirable framerate for modern games to achieve.


### 3.3 Design constraints

Unfortunately, the full scope of the project was not attainable within the time frame. Whilst this
does mean that some of the expected features, such as a 2d version of the program, were not
achieved, it also opens up areas for future expansions if the project were ever to be built upon. The
primary issue surrounding the constraint of building a 2d project was the misunderstood value of the
objective. During the planning phase of the project a 2d model was highlighted as a good entry point
into the flocking model, as it would supposedly be simpler to begin with, and work as a good entry
point in case there were issues with the 3d model. However, upon reflection it was realised that
such a model would actually pose no reduction to difficulty, as the 3d space only added one extra
variable (a z axis) and, if anything, would simply increase the work load (due to the way unity
handles 2d and 3d games separately). It was therefore decided that this would be cut from the
project so that the main deliverable can be focused on.

As listed above, Unity posed a constraint for development. The interface was confusing during the
learning phase, as it can be overwhelming due to the number of options and the very specific
function that most provide. Furthermore, due to its very rigid systems, it often requires that actions
be taken deliberately and perfectly, or else issues arise. This is reflected in how it treats project
versions. Because the project was started in one version, future users will have to install the LTS
edition of that year in order to operate the software. This may pose a problem when using the code
later, as newer versions of unity tend to have questionable support for older models due to changing
architecture.


## 4 Design

### 4.1 Software design

The approach taken towards designing this programming was to split the code into two distinct
classes.

The first class; the boid class, is responsible for the actions of a single individual boid. It holds the
algorithm that all boids will use when calculating their movement for each update. The second is the
Boid Manager class, which creates every instance of the boids and passes them the important
values. Below is a class diagram that illustrates the core values and methods of both classes

```
Figure 8: A UML class diagram showing the interaction between the two classes – Personal collection
```
As the diagram above shows, the BoidManager class is responsible for instantiating any number of
Boids and passes through several of the variables it created. The boid class also takes note of the
current instance of the BoidManager so that it can access any public variables from it, such as the
weightings, cage size, speed and other such important variables for the boid calculations.

When the code is run, the BoidManager will loop through the instantiation until enough boids have
been made. The Update loop then runs through the Boids, moving them as appropriate, whilst the
BoidManager continues to draw. When the program ends, these values are unloaded and the code
ends. The following activity diagram visualises the codes run cycle.


```
Figure 9: A UML activity diagram showing the codes run cycle from start to end – Personal collection
```
As the diagram illustrates, the flow of the program is rather sequential, moving from one class to
another without many call backs, save for the few variables that the Boid class requires from the
BoidManager. The Boid loop is only ever concluded when the program is closed, so typical runs are
very straight forward in that they start with the BoidManager class, move to the update loop, and
then are eventually closed.

### 4.2 Experimental design

The methods of testing that are required for this project are not as experimental as is the case in
other projects. The main focus is tracking the system performance and where the issues arise. If the
problems can be highlighted (such as methods that are draining system resources or which
component is holding back the program), then they can be tackled, either by reducing load on the
system component or using the knowledge of which method is the most system intensive to focus
the optimisation.

As such, these tests will be conducted utilising the following tools; Unity’s profiler tool, task
manager’s performance tab and the Resource Monitor. When all three tools are combined
unanimously, the strengths and weaknesses of the program, as well as the overall performance, can
be analysed to a high degree. The reasons behind each tool are detailed below.

Unity’s profiler tool is the key tool for testing the project.


```
Figure 10: Screenshot of unity profiler – Personal collection
```
The profiler tool shows a display (pictured above) with the current FPS, and the performance
difference between the nearest benchmark (30, 60, 120 etc) and the second closest benchmark FPS.
This allows developers the ability to see the difference in performance required to reach a goal. On
the side it has options for which measures should be displayed on the graph, such as the usage of
rendering, animation and scripts. This allows the developer to distinguish between different
resource drains and in doing so find where the program is using the most resources. If a program
were to use a lot of animation this would be a handy graph to have displayed, however as this
project is light on graphics this graph has been disabled, as it would provide no useful data. Lastly
the bottom of the tool has a hierarchy chart in which different methods can be investigated for their
CPU drain, allowing the developer to go directly to the issue rather than having to try and guess
where the problems could stem from.

The profiler also has several other graph options, such as memory (RAM), Rendering, Physics, and
several others that are irrelevant to the project. These graphs will also be taken into account when
considering where the largest drain on resources comes from, although an understanding of the
project leads the logical answer to be that the CPU will have the highest system drain.

Windows’ task manager is also a useful utility tool during testing, as it allows the developer to view
the strain placed on various components to see if any are being bound. Foremost, this comes in the
performance tab, which allows the user to view each component’s current usage, pictured below.


```
Figure 11: Performance tab of task manager – Personal collection
```
When utilised during high intensity testing, it can help reaffirm which component is being maxed
out. Whilst this is viewable within the profiler tool to an extent, the performance tab can help
confirm this. When used in conjunction with the resource monitor, it can also highlight any
unexpected issues with the program.

The resource monitor in Windows provides a similar benefit to Task Manager, especially when used
in direct conjunction. The reason it has been listed separately is due to its ability to show the
performance and strain on individual CPU cores, which task manager does not offer.

```
Figure 12: Resource Monitor – Personal Collection
```

Pictured above is the resource monitor interface. As can be seen on the right-hand side of the
screen, each core is listed, with the performance of each core and the usage listed in graph form.
This proved instrumental when testing, as it enlightened us to a primary issue with the program,
which is that it has no multi-threading ability. If it did, the core performance would be far more
spread out which would spread the load across the full power of the CPU, allowing it to sustain a
much higher intensity.

As well as track the system resource drain through these tools, it also helps ensure that no
background programs are eating into resources that should be dedicated to the program during
testing. Whilst a minor detail, it can make an impact when high intensity programs like chrome are
taking up resources in the background.

The final detail worth considering are the specifications of the computer used to compile and test
this project. The Processor (CPU) is a Ryzen 5 5600X, meaning its clock speed across all cores
averages around 4.30 GHz with 6 cores. This is important to consider, as without any multithreading,
only one core will be utilised. As of the writing of this report, this CPU is a higher end product, and as
such the data recorded here will be reflective of a high-end system with strong components.
Performance on a lower budget system is, unfortunately, not currently within the scope of the
experiment due to a lack of access to any other computer systems. Likewise, the RAM installed in
the system is DDR4, running at roughly 3000 MHz, with 32GB, so it is unlikely that the tests would be
bound by the RAM.


## 5 Implementation and testing

### 5.1 Implementation

The first major decision made during the project was deciding what system would be worked in to
develop the project. It was settled on that ‘Unity’ would be the chosen software to develop in, as it
offers a large amount of support for asset importing and management, as well as offering a solid
framework for the purposes of the project. As discussed previously in the interfaces section, the
layout of Unity’s components interface offers perfect support for developers looking to stress test
the variables of their program on the fly, and as such was a perfect fit for this project. Whilst it was
confusing learning to work with the program at first, after an adjustment period and getting used to
the software it became immensely rewarding to use, and if this project were to be recreated unity
would definitely be a solid suggestion for a framework (especially if the programmer has prior
experience).

The initial architecture that was designed was a simplistic one that would check the locations of all
boids and compare them to the agent currently being evaluated. This is obviously not an efficient
architecture, but was far simpler as an initial setup. The plan was to move from here towards an ECS
(Entity Component System) architecture, however it occurred that this may be too reliant on the
systems put in place by unity. Whilst utilising frameworks is acceptable for a project such as this, if
they are relied upon too heavily then the technical achievement of the project would suffer
inversely.

As such, the structure of the algorithm ended up as follows; the BoidManager checks the number of
boids needed and runs a for loop to create the correct number of boid instances. These boids are
initialised and their update loop is started, which checks where the boid should move next and then
moves there. This continues until the code is closed and the update loop is ended.

### 5.2 Testing

The first set of tests performed were to find out the benchmarks for varying quantities of boids. This
was achieved by running the software for several seconds to ensure the initial calculations were
complete, then pausing the software and consulting the provided framerate that unity displays in
the statistics section. Using this data gave a solid benchmark for the data. This could then be
expanded upon with a further set of tests to identify the exact quantities of boids for the two goals
(30 frames and 60 frames respectively). This data was compiled into a chart shown below. If you
wish to view the full set of data it has been included in the appendix of this report.


```
Figure 13: Visualisation of testing data – Personal collection
```
The chart above shows the frame rate (Y-Axis) when the software is run with a varying number of
Boids (X-Axis). This chart is a helpful demonstration of the impact that an increase in boids has, as
well as an effective way to highlight the number of boids that can be run at varying framerates. It is
worth considering that the performance change from 100 to 200 and 200 to 300 is not comparative.
The fps at 100 boids was 312.7, but dropped 211.5 frames, around a third of its total framerate
when the number of boids was doubled. However, when it was double again to 300, the framerate
only dropped to 51.9. This drop, whilst a substantial half of the previous framerate, is still less of a
drop in proportion than before.

The next tests to be conducted concerned where the system resources were being dedicated.
Whilst the capacity of the program had been worked out, it was important to know where the issues
lay so that further adjustments could be made, either within the scope of the project or in later
improvements made. This could be achieved by utilising Unity’s system profiler to view the various
methods and Unity systems to see which was using the most system resources. Below is a
screenshot of the profiler when 375 boids are being run at 30 FPS.


```
Figure 14: Unity Profiler during a moderate intensity (375 boids) run of the software - Personal
collection
```
The ticked boxes on the left-hand side correlate to the processes being tracked. For simplicity the
other 4 have been unchecked to reduce screen clutter, as these caused no system drain. As figure 14
shows, the highest system requirement are the scripts, followed then by the ‘Others’, with
Animation, Rendering and Physics pulling very few system resources to themselves.

This information tells the developer that the current issues to be analysed are the scripts. In the
case of this software this was the likely outcome, as the program is heavily reliant on the Update
method within the scripts whilst simultaneously being very low on visual elements or design.

The next thing to do is analyse which scripts are causing the system drain. The section at the bottom
of figure 14 is a hierarchy chart that allows the user to explore the total CPU drain. Expanding on one
of the drop-down options imparts more insight into this.

```
Figure 15: Unity Profiler’s hierarchy expanded – Personal collection
```
As can be seen in the image above, the Update method within the Boid script is the largest drain on
the system followed by the camera. Looking into the raw hierarchy shows that this is because each
individual boid is using around 0.2% of the programs allowed CPU resources (pictured below).


```
Figure 16: Unity profiler raw hierarchy – Personal Collection
```
This tells us that, if improvements were to be made to the software, the primary focus should first
and foremost be the Boid script. This method of testing was extensively used during early
development to highlight the core issues within the program, although it quickly became obvious
that the issues were recurring.

The unity profiler also allows an analysis of Memory drain. For the purposes of this software this did
not yet arise as an issue, as currently the CPU was the largest drain, however eventually if efficiency
had continued to be improved there would have been a turning point where memory became the
system drain. The display for this section of the profiler is as such.

```
Figure 17: Unity profiler Memory resources – Personal collection
```

The above image shows that the system is assigned a maximum of 1.31 gigabytes, of which the
system is using 1.17. It then breaks down where the tracked memory is being allocated, largely being
graphics and the managed heap. Currently this is not an issue, as the software is not draining enough
of the allocated ram to cause any issues, and as there is plenty of reserved ram unassigned, the
assigned amount could have been upped for testing. However, it is important to keep track of this as
efficiency of algorithms is improved.


## 6 Evaluation and discussion of results

The primary goal of this project was to develop a flocking algorithm that could push the number of
independent agents to a high degree to see how many are possible.

This was achieved to a moderate degree. The final number of boids possible with the architecture
used was 270 at 60 fps and 375 at 30. Whilst this is a considerable number, further research has
shown that utilising unity’s ECS and Job systems allows for an incredibly high number of independent
agents (reports have ranged from 5.000 to 100,000). As explained earlier in the report, ECS was
avoided on the account of wanting to keep the heavy lifting of the project on the side of the project
leader. If the original goal had been to design a game using flocking architecture, then it would have
been reasonable to expand on the code using ECS architecture.

Nevertheless, there are ways to expand on the code without relying on unity’s provided methods. If
this project were to be altered in the future, it would be advised that Octrees or Quadtrees be
analysed as a potential method to decrease calculation time by focusing on specific quadrants and
not overloading the update calculations. This would massively increase efficiency as the update
would only ever check boids that are in the same quadrant and not each boid in the scene.

To counter this however, it is worth considering that, whilst Octrees or Quadtrees would
significantly improve performance early on during the initial period of boids seeking out and finding
flockmates, as the size of the main flock grows, the impact of implementing quadtrees will inversely
decrease, as the boids will begin checking every nearby boid, and in doing so return to the original
structure of the program. Therefore, whilst it would still be advised that any future work on the
project explore this avenue, it is also worth considering that in a small enclosed environment like the
current default scene, this change would not improve performance consistently. The same cannot be
said however for if this code were to be implemented in a space where all boids are unlikely to cross
paths, and flocks instead were more separated.

It should also be considered that, as detailed earlier in the report, the system the project was tested
on was a very high specification system. In the future if this project were to be tackled again, testing
should be far more expansive to encompass lower specification systems. This could be achieved
quite easily by utilising provided university computers, or requesting friends run given tests on their
own systems to test the compatibility. Whilst these were feasible options, due to time constraints it
was not deemed necessary, as there was already an understanding that the software would lose
performance on weaker systems.


Following is a discussion of the primary objectives and how each was tackled, to what degree it was
tackled and a reflection on how it could be improved.

### 6.1 Objective 1: Create and implement a 2d swarming AI

The first objective, focused on creating the algorithm and implementing it within a two-dimensional
environment, was achieved to a moderate degree of success, with some slight alterations made to
expectations. Overall, the core algorithm was successfully created and implemented. As discussed
earlier, the idea of a 2d implementation was eventually decided against, as ultimately it would be a
waste of efforts when there were more beneficial additions to be made, however the other
secondary objectives that would lead to the completion of this objective were both possible and
completed. Therefore, whilst the namesake of the objective was not complete, the actual core
content was, so this can be deemed a moderate success. In the future this objective would not need
to be tackled any differently, as the 2d provided no benefit.

### 6.2 Objective 2: Move the AI into a 3d space

As the code began in a 3d scene, this objective was achieved far earlier than expected. Therefore,
the primary objective here was completed. The secondary objectives can be divided into two
sections, avoiding walls within a 3d space, which was achieved, and aesthetic changes to the scene,
such as an underwater theme and fish assets for the boids. This latter objective was not completed.
Whilst it was within the scope of the project to do, as it posed no technical challenge, it ultimately
felt like wasted time, as visual updates would not award any amount of technical achievement and
could even hinder performance depending on the level of change made (one idea was to include a
swimming animation which definitely could have reduced performance). Therefore, it was decided
that this should be scrapped as it is not necessary. It can therefore be said that this objective was
completed to a moderate degree, as the important elements were focused on. However, there were
still secondary objectives that were abandoned in the process.

### 6.3 Objective 3: Ensure the code is efficient

This objective can be measured by the performance of the program. The algorithm made was
streamlined as best as possible considering its structure, however, its inherent design is not the most
efficient option. Whilst efficiency was prided, it is also worth considering that looping through each
boid during the calculations would be a massive hit to efficiency. Therefore, this objective was not
completed to the degree that would have been liked. The code works, but the number of boids
displayed could definitely have been higher if other techniques were explored more fully.

### 6.4 Reflection

Whilst not all of the original objectives laid out during the project’s initiation document were
completed to the degree that was hoped for, the core functionality of the program is still in place.
The software is capable of creating a swarm of flocking agents that will independently seek out
fellow boids to flock with.

### 6.5 Further work

Moving forwards, the software could be improved by looking into different, more complex and
advanced algorithm structures. It could be worth revaluating the use of unity’s ECS architecture to
see if it would benefit the technical achievement of the project.

Furthermore, applying the algorithm within a game environment could be beneficial, as it would
provide the project with more purpose behind it.


## 7 Conclusion

Whilst many of the project’s objectives were achieved, the algorithm architecture undertaken in this
project was not the most efficient approach. The software is highly modular and can be applied in
various media, such as video games or simulations. However, for higher intensity uses of the
program, there would need to be reassessment of the algorithm used to make it more efficient so as
to not drain the performance of these other applications. The report has laid out the issues found
and documented the process well, such that in the future it would be possible to recreate the
project given what is written here. The initial planning did not fully account for the actual hardships
that would be faced during the programming, nor was it aware of which parts of the program would
cause difficulty during creation. Time planning for the programming was followed well, however
during the write up process a lot of issues arose within the personal life of the project leader,
causing huge delays which in turn caused a large time pressure on the report. Obviously, this could
not have been predicted, but if this project were to be reattempted more time should be dedicated
to the report or, conversely, the project should be started earlier to allow for this.


## References

Reynolds, C., (1987). Flocks, Herds, and Schools: A Distributed Behavioral Model. _ACM SIGGRAPH
Computer Graphics_ , 21(4), pp.25-34.

Reynolds, C., (1995). _Boids (Flocks, Herds, and Schools: A Distributed Behavioral Model)_. [online]
Red3d.com. Available at: <http://www.red3d.com/cwr/boids/> [Accessed 13 April 2022].

Tiemann, B., 1994. _The Lion King: Film Notes_. [online] Lionking.org. Available at:
<https://www.lionking.org/text/FilmNotes.html> [Accessed 15 April 2022].

Delgado-Mata, C., Martinez, J., Bee, S., Ruiz-Rodarte, R. and Aylett, R., 2007. On the Use of Virtual
Animals with Artificial Fear in Virtual Environments. _New Generation Computing_ , [online] 25(2), pp.145-

169. Available at: <https://link.springer.com/article/10.1007/s00354- 007 - 0009 - 5> [Accessed 15 April 2022].

Hartman, C. and Benes̆, B., 2006. Autonomous boids. _Computer Animation and Virtual Worlds_ ,
[online] 17(3-4), pp.199-206. Available at: <https://onlinelibrary.wiley.com/doi/10.1002/cav.123> [Accessed 15 April
2022].


## Appendix A – Full set of data from the tests

Full set of data for the tests.

```
Number of Boids Framerate
1 1050
10 895
50 614
100 312.7
200 101.2
300 51.9
400 28.1
500 18.4
```
```
Number of Boids Framerate
225 84.9
250 71.8
275 60.1
```
```
Number of Boids Framerate
325 41.8
350 37.7
375 30.8
```

