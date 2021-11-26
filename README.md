# MLAgents_FirstSteps

The following is an introduction to the use of MLAgents.

## Section 1 : Install the requirements

Create a virtual environment in Python and install the requirements by entering these command lines in your terminal :

``
> python -m venv .env
> .env/Scripts/Activate.ps1
> pip install -r requirements.txt
``

## Section 2 : Using the environment

In this tutorial we will leave aside the creation of the environment.
Firstly, import the project *Tutorial* in Unity Hub.
![](/resources/1.png)
![](/resources/2.png)

Then run this command in your terminal :
``
> mlagents-learn.exe --run-id=0
``
When a message indicates that the terminal is listening to the game, please launch the game from the editor.
![](/resources/3.png)
![](/resources/4.png)

That is it ! Now you can run an agent on a Unity game !

If you want to restart a run, you can either :
* use ``mlagents-learn.exe --run-id=x`` with a different id
* use ``mlagents-learn.exe --run-id=0 --force`` to restart agent 0 (will not remember what has been learned)
* use ``mlagents-learn.exe --run-id=0 --resume`` will resume the training