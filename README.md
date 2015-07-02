# Dromund Kaas II
Dromund Kaas team - C# game for the C# OOP course in SoftUni.

# CAVEMAN
##There is light in the darkest places.

### The Game:

* You are a survivor!
  * Survive waves of zombies!
  * Start fires, stay in the light and avoid the shadow!
  * Live!

Included:
* ONE WHOLE LEVEL!
* One primary profession.
* Various attack and defense skills.
* Terrifying zombies out for your brains!

### Controls:
GamePad:
   Movement: D-Pad
   Actions: A-B-X-Y-Bumper
   Pause: Back
   Interact: Select
   Quit: Back + Select

Keyboard:
   Movement: WASD
   Actions: 1-2-3-4-5
   Pause: Space
   Interact: F
   Quit: Escape

### Technical Features:

* Easily extendable code:
  * Fully documented.
  * Fully OOP-driven.
  * Neatly formatted.
* A wealth of options for extension, including character classes, custom engines, custom skills, and advanced character handling.
* Reusable and extendable classes.
* Great to tinker and polish.

---

### [MonoGame](http://www.monogame.net/)
... е framework за правене на игри. Направено е да работи подобно на XNA.

#### Как се наглася и използва MonoGame:
* Инсталирате си MonoGame - http://www.monogame.net/downloads/ - интересува ви последната версия. Ще намерите опция за Visual Studio.

 => Готови сте!
 
Вече би трябвало да можете да си направите проект за MonoGame директно от Visual Studio.

* Документация - http://www.monogame.net/documentation/?page=main

---

### Git

#### Как се наглася и използва Git (cmd версия):

* Инсталирате Гит - http://git-scm.com/ - тази стъпка е нужна независимо дали ще използвате CMD или TortoiseGit

0. Конфигурирате Гит на машината си (в cmd):

      `git config --global user.name "YOUR NAME"`
    
     `git config --global user.email "YOUR EMAIL ADDRESS"`
     
1. Отивате в директорията, където ще си правите локална репозитория.

     `cd C:\baba\`
     Ако е на друг драйв, пишете първо `D:`, за да се преместите на него. След това - `cd ...`.
     
2. Инициализирате Гит

     `git init` => създава ви се скрита .git папка.
     
3. Казвате "това място е за онова място":

     `git remote add origin ---url---` - ще наричаме origin отдалечената репозитория; URL заменяте с clone url-a, който Гитхъб ви дава.
     
Готови сте!

=> Как се работи с Гит?

Интересуват ви 4-на команди:

1. `git pull -u origin master` - с други думи, `git pull` от origin в master. (или дърпаме нещата при нас)
2. `git add .` - прибавяме всички променени файлове в нов комит, готов за пращане. Ако искаме специфични да прибавим, пишем ги поименно вместо точката.
3. `git commit -m "blablabla"` - приготвяме комит (принос към репозиторията в Гитхъб) със съобщение "блабла". Важно: не пишете "блабла", ами конкретно какво променяте!
4. `git push` - качвате всичко в главната репозитория. Ако тя е на по-нова версия от вас, трябва първо да направите `git pull ...` - и ако има конфликти, трябва да се справите с тях. Как става това - четете.
5. `git status` - когато се чудите какво става с гит. Чисто информативно.

И това е!
пушвайте ;)
