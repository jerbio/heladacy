
<!-- TABLE OF CONTENTS -->

<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#what-is-heladac">What is Heladac?</a>
    </li>
    <li>
      <a href="#implementation-details">Implementation details</a>
    </li>
    <li><a href="#how-to-launch">How to launch</a></li>
  </ol>
</details>



## What is Heladac?

Heladac is a credential manager, it helps generate credentials for a given page. It is also a random email generator. There are two components the heladac website and the heladac extension. This site is the heladac site.
Etymology = Helm of Hades. 


## Implementation Details
Heladac is built on .net core stack and [react js](https://github.com/facebook/react). The email protocols are handled by Haraka and this repo handles the user email and credential management services. Being a .net core stack we use all asp.net controller services to handle the services also it means you need to have the .net clr installed. The core folder is [HeladacWeb](https://github.com/jerbio/heladacy/tree/main/HeladacWeb). Within this folder the reachjs app is in the folder [ClientApp](https://github.com/jerbio/heladacy/tree/main/HeladacWeb/ClientApp).


## How to Launch
Since this is a .net core & reactjs web app this has all the default launch parameters/procedure of .net core app. We strongly suggest using [Visual Studio 2019 or later](https://visualstudio.microsoft.com/)(not vs code) to launch the solution. You can execute the [heladacy.sln]( https://github.com/jerbio/heladacy/blob/main/heladacy.sln) file. Once you launch the file you can launch the web app with ctrl+f5. 

If you do not have visual studio installed then you’d need to install [dotnet](https://dotnet.microsoft.com/download) for your OS. 
Once installed you’d need open the HeladacWeb folder in your terminal and then execute the command ‘dotnet run’.

@2j3rbi4



