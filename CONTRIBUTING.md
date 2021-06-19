# Contributing to LTW Project

We're happy that you have chosen to contribute to the LTW project.

To organize the efforts made for this game, the wotoTeam has written this simple guide to help you.

Please read this document completely before contributing to LTW.


## How To Contribute

LTW has a `master` branch for stable releases and a `develop` branch for daily development.  New features and fixes are always submitted to the `develop` branch.

If you are looking for ways to help, you should start by looking at the [Help Wanted tasks](https://github.com/TeaInside/LTW/issues?q=is%3Aissue+is%3Aopen+label%3A%22Help+Wanted%22).  Please let us know if you plan to work on an issue so that others are not duplicating work.

The LTW project follows standard [GitHub flow](https://guides.github.com/introduction/flow/index.html).  You should learn and be familiar with how to [use Git](https://help.github.com/articles/set-up-git/), how to [create a fork of LTW](https://help.github.com/articles/fork-a-repo/), and how to [submit a Pull Request](https://help.github.com/articles/using-pull-requests/).

After you submit a PR, the wotoTeam will build your changes and verify all tests pass.  Project maintainers and contributors will review your changes and provide constructive feedback to improve your submission.

Once we are satisfied that your changes are good for LTW, we will merge it.


## Quick Guidelines

Here are a few simple rules and suggestions to remember when contributing to LTW Project.

* :bangbang: **NEVER** commit code that you didn't personally write.
* :bangbang: **NEVER** use decompiler tools to steal code and submit them as your own work.
* :bangbang: **NEVER** decompile another games' assemblies and steal another companies' copyrighted code.
* **PLEASE** try keep your PRs focused on a single topic and of a reasonable size or we may ask you to break it up.
* **PLEASE** be sure to write simple and descriptive commit messages.
* **DO NOT** surprise us with new APIs or big new features. Open an issue to discuss your ideas first.
* **DO NOT** reorder type members as it makes it difficult to compare code changes in a PR.
* **DO** try to follow our [coding style](CODESTYLE.md) for new code.
* **DO** give priority to the existing style of the file you're changing.
* **DO NOT** send PRs for code style changes or make code changes just for the sake of style.
* **PLEASE** keep a civil and respectful tone when discussing and reviewing contributions.
* **PLEASE** tell others about LTW Game and your contributions via social media.


## Decompiler Tools

We prohibit the use of tools like dotPeek, ILSpy, JustDecompiler, or .NET Reflector which convert compiled assemblies into readable code.

There has been confusion on this point in the past, so we want to make this clear.  It is **NEVER ACCEPTABLE** to decompile another games' copyrighted assemblies and submit that code to the LTW Game project.

* It **DOES NOT** matter how much you change the code.
* It **DOES NOT** matter what country you live in or what your local laws say.  
* It **DOES NOT** matter that another games' are discontinued.  
* It **DOES NOT** matter how small the bit of code you have stolen is.  
* It **DOES NOT** matter what your opinion is of stealing code.

If you did not write the code, you do not have ownership of the code and you shouldn't submit it to LTW Project.

If we find a contribution to be in violation of copyright, it will be immediately removed.  
We will bar that contributor from the LTW project.

## Code guidelines

Due to limitations on private target platforms, LTW enforces the use of C# 9.0 features.

It is however allowed to use the latest class library, but if contributions make use of classes which are not present in .NET 5.0, it will be required from the contribution to implement backward compatible switches.

These limitations should be lifted at some point.

## Licensing

The LTW project is under the [MIT License](https://opensource.org/licenses/MIT). 
See the [LICENSE](LICENSE) file for more details.  
Third-party libraries used by LTW are under their own licenses.  Please refer to those libraries for details on the license they use.

We accept contributions in "good faith" that it isn't bound to a conflicting license.  By submitting a PR you agree to distribute your work under the LTW license and copyright.

To this end, when submitting new files, include the following in the header if appropriate:
```csharp
// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.
```

## Need More Help?

If you need help, please ask questions on our [Telegram community](https://t.me/LTW_Game) 
or come and join our [Discord Server](https://discord.gg/Nxd9xs4PbN).


Thanks for reading this guide and helping make LTW great!

 :heart: wotoTeam
