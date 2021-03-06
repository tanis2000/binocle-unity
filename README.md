Binocle for Unity
=================

Binocle is a simple 2D code-based framework for Unity.

I am a programmer and I like the cross platform nature of Unity, but I find it hard to quickly code 2D games having to resolve around the editor. 
I made Binocle to help me work in a more efficient way.

Features
========
- Entity Component System
- Sprite animations
- Pixel perfect camera
- Object pooling
- AI system (Behavior trees, Finite State Machines, Goal Oriented Action Planning, Utility based)
- A* path finding
- TexturePacker support
- Bitmap fonts importer
- Version control automation

Current status
==============

This is an early alpha version. APIs are subject to breaking changes. 
I have already shipped a few games using this framework but it lacks proper documentation. Most of the concepts you find in Nez and Monocle still apply to Binocle.
Binocle for Unity is roughly based on my Binocle engine which is a fully featured cross platform C++ 2D game engine. Nez and Binocle share a lot of inner workings as we developed them at the same time and me and Mike exchanged ideas frequently.
Binocle for Unity only offers a subset of Binocle as most of the core systems used are those provided by Unity.

Getting started
===============

Binocle needs a few steps to setup correctly so I packaged a starter project to use as the base for your own project.

The starter project is available here: [Binocle Unity Seed](https://github.com/tanis2000/binocle-unity-seed)

Credits
=======

Many of the concepts come from Matt Thorson's Monocle engine (the name of this project is a kind of joke around Matt's engine name as you can guess).
The ECS is based on the excellent artemis-odb.
I have blatantly taken the AI code from Prime31's Nez and adapted it to my needs.
Other pieces of code have been taken here and there on the web and I can't recall where they come from. If you see some code that looks familiar, please let me know and I'll give full credits.

License
=======
The MIT License (MIT)
Copyright (c) 2016 Valerio Santinelli

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.