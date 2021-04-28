# Sword Art Online : Last Testament 
====================================

Welcome to the LTW source code! 

Sword Art Online Last Testament, is a open-source MMORPG game.
From this repository you can build LTW game, all of the Contents are included.



[![Join the chat at https://discord.gg/Nxd9xs4PbN](https://img.shields.io/discord/503992242625576982?color=%25237289DA&label=DOT&logo=discord&logoColor=white)](https://discord.gg/Nxd9xs4PbN)


 * [Supported Platforms](#supported-platforms)
 * [Support and Contributions](#support-and-contributions)
 * [Source Code](#source-code)
 * [License](#license)


 ## Supported Platforms

 We support only Windows and Linux right now.
 If there is a platform we don't support, please [make a request](https://github.com/TeaInside/LTW/issues) or [come help us](CONTRIBUTING.md) add it.

 * Linux
   * Ubuntu 20.04
   * Ubuntu 19.10
   * Ubuntu 19.04

 * Windows
   * Windows 7
   * Windows 8
   * Windows 8.1
   * Windows 10


## Support and Contributions

If you think you have found a bug or have a feature request, feel free to use our [issue tracker](https://github.com/TeaInside/LTW/issues). Before opening a new issue, please search to see if your problem has already been reported or not.  Try to be as detailed as possible in your issue reports.

If you need help using LTW or have other questions we suggest you to join our [telegram community](https://t.me/LTW_Game).  Please do not use the GitHub issue tracker for personal support requests.

If you are interested in contributing fixes or features to LTW, please read our [contributors guide](CONTRIBUTING.md) first.

**To get started using GitHub:**

- Create your own LTW **fork** by clicking the __Fork button__ in the top right of this page.
- [Install a Git client](http://help.github.com/articles/set-up-git) on your computer.
- Use the GitHub program to **Sync** the project's files to a folder on your computer.
- Open up **LTW.sln** in your IDE.
- Modify the source codes and test your changes.
- Using the GitHub program, you can easily **submit contributions** back up to your **fork**.
- Do not **commit to master**, for each feature **create new branch**.
- When you're ready to send the changes to the LTW repo for review, simply create a [Pull Request](https://help.github.com/articles/using-pull-requests).

**Advanced topics:**
- You can update your master branch by executing:
  - git pull https://github.com/TeaInside/LTW.git master
- If your master is tainted and any branch you make contains junk, you can do **hard reset**. All unmerged commits on master branch will be lost.
  - git checkout master
  - git fetch https://github.com/TeaInside/LTW.git master
  - git reset --hard FETCH_HEAD
  - git push --force origin master


## Source Code

The full source code is available here from GitHub:

 * Clone the source: `git clone https://github.com/TeaInside/LTW.git`
 * Open the solution `LTW.sln` or project file `LTW/LTW.csproj`

For the prerequisites for building from source, please look at the [Requirements](REQUIREMENTS.md) file.

## License

The LTW project is under the [MIT License](https://opensource.org/licenses/MIT).
See the [LICENSE](LICENSE) file for more details.  
Third-party libraries used by LTW are under their own licenses.  Please refer to those libraries for details on the license they use.