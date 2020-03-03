---
layout: module
title: Digital Ocean Setup
resource: true
categories: [ install ]
description: Installing the system on Digital Ocean.
---

Digital Ocean is a good solution if you want a self-hosted option.

> If you use [this referral link](http://aresmush.com/install-ares/digital-ocean/www.digitalocean.com/?refcode=5c07173bc1f2), you will get a $10 credit and I will get a referral bonus that helps keep the aresmush.com website up.

## Creating a Droplet

"Droplet" is just what Digital Ocean calls a server.  Once you have an account, here are the options you'll need to select when creating your droplet:

* Use the "Ubuntu" distribution image.
* The smallest server size (512MB RAM) is adequate if you just want to run a single game. You can get a bigger size if you want to put other stuff on it.
* Enter a hostname of your choice.
* The other options can be left as the default.

You will be sent an email with your server's IP address and the username/password of the root user.

You can connect to your server using the Digital Ocean website or a telnet application like PuTTY for Windows or Terminal telnet for Mac.

## Getting a Domain Name

DigitalOcean only gives you an IP address.  While that's enough for your game to run, many games prefer to have a domain name too (like mymush.com).  

You can get a domain name pretty cheap at [Namecheap.com](http://namecheap.com).  Once you have one, follow [this tutorial](https://www.namecheap.com/support/knowledgebase/article.aspx/319/78/how-can-i-setup-an-a-address-record-for-my-domain) to point the main "A Record" of your domain at the IP address of your MUSH server.  It may take a few hours for this to take effect, but then your domain will be set up.

## Setting Up the Server

The root user is a special administrative account that's created for you automatically.  The password will be emailed to you.

These instructions will create a 'mush' user for your everyday use, and install some programs that PennMUSH needs.

    adduser --disabled-password --gecos "" mush
    apt-get update
    apt-get install gcc
    apt-get install libpcre3a
        If libpcre3a doesn't work, the next one should cover you.
    apt-get install libpcre3-dev
    apt-get install libssl-dev
    apt-get install make
    apt-get install emacs
    apt-get install unzip
    apt-get install build-essential
    passwd mush
       Enter a password and write it down.  You'll need it in the next section.
    usermod -a -G sudo mush

## Installing PennMUSH

Now exit from the server and log in again with the 'mush' user and the password you just created.

    git clone https://github.com/pennmush/pennmush.git
    cd pennmush
    git checkout 186p0
    ./configure
    make update
    make install

> Note: This will install PennMUSH v1.8.6p0, which is the version that the starter DB is based on.  If you specify a different verison, you may have conflicts with the mush.cnf file.  It may work, but it may not.  Try it at your own risk.

## Installing FS3

    cd game
    mkdir starterdb
    cd starterdb
    wget -O starterdb.zip https://github.com/lynnfaraday/MUSH/blob/master/farasoftcode/Releases/FaraMUSHCode%20Starter%20DB%20-%208.2%20for%201.8.6%20p0.zip?raw=true
    unzip starterdb.zip
    cp mush.cnf ..
    cp restrict.cnf ..
    cp *.gz ../data
    cd ..

## Editing the Configuration

Use your favorite unix text editor (pico/emacs/vi) to edit `mush.cnf` and set:

* MUSH name
* Port number (if you want to change it from the default 4201)
* Host name

## Starting the Game

From the mush game directory, type:

    ./restart

Your game should be running!

## Default Characters

The starter DB comes with the usual Penn #1 character (One), who has no password.  It also has a Gamewiz character with a default password of `wizb00ts`.  

> You should log in and change both these passwords immediately to something more secure.

## Setting Up the Wiki

The FS3 codebase has various built-in hooks to work with wikidot, a free wiki hosting service.  The [MU Wiki Template](http://muwikitemplate.wikidot.com/) is a template you can use.  Either copy the page code piecemeal, or use the "Clone This Site" button to copy the entire site and make it your own.