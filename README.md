# GD Guesser
A small purely UI Godot game which can run on Android (and Windows, but it's meant to be played on a phone)

![Start screen](https://i.imgur.com/fytuil9.png)
I'm not the best at UI, but it works at least!

## How to play
1. You need to create a themed pack. Some example: Animals, 90's songs, inside jokes of your friend group, etc.

2. Fill the pack with prompts. Animal pack will contain animals. Or won't. It's really up to you how you make your packs.

3. Select the pack, customize the settings, and play.

4. Player holding the phone can't see the screen. All other people have to explain the prompt shown on the screen without
using the words from the prompt.

5. If the player guesses, he has to swipe right. There's a timer. If it runs out, he doesn't get a point.
You can swipe left to skip a prompt.

6. The more prompts you guess, the better. Have fun.

## About the project
I made this small game mainly for myself, to practice Godot and MVVM.

I made my own version of MVVM. I call it "my own version because" it's easier than saying "I didn't understand MVVM fully and made a mess which I didn't want to rework". I'll make it better next time.

I learned how to use the theme editor, which I like, but sometimes it's annoying (like forgetting to make a stylebox unique and than having coupled UI styles. This one might be on me as I call all of them StyleBoxFlat).

I learned how to use resources as a way to store game data. Be aware that there is no system to back up your prompts if you switch phones. You can do this manually, but you'll have to get into the application data folder on your android,
and locate the game folder. Should be called something like **app.theronguard.gdguesser?** You can copy it, or even modify it with a text editor to add prompts. On Windows it should be stored in **%appdata%/godot/app_userdata.**

This could be expanded and polished, but it won't be, as this was just a project to grasp some basics before venturing out to a bigger one.
