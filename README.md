Forest of Zarn
==============
Forest of Zarn is Complex "Choose your own Adventure" game, that barrows classic role playing elements, a three dimensional character viewer, and the best part; customizable adventurer files. The main goal of the project was to develop a game whose story and content can be edited with ease and without redistributing the client for each story update. Exposing the story and its elements in a naked XML means that anyone one can create there own story and share with friends. In addition to making the games story and elements exposed was to make them easy to digest and use so that it's not limited to people who can read and write code. I hope to improve upon the base code that's used in the game to allow more flexibility and diversity for greater story telling and entertainment.

The scripts and tools that I have created for the use of the game are free to use as by license, just note that some of the scripts for UNITY 3D are for reference and that I will NOT be uploading the meta-data for the scripts. Also copies of the game will be given out as per request since it's still in a development state and not ready for a public release.

Hopefully you may find enlightenment within the code provided...

# Story XML API

The narrative and functional force of the game is the StoryExample document. This file is what is call an "Extensible Mark-up Language" or XML for short and it's a compromise between a human and computer for readability. I have designed this XML document to be clean and visual way to create a Role Playing Game without needing to get to deep into code and a way to expand a story without having to redistribute the game.

Provided is a "Living List" of XML tags that are for the games story with a description and example.

## Data Tag
This is the encapsulating tag for the whole document or "Root" as it where. There is only ONE tagged pair in the document, any more will confuse the computer. All other tags must be between the start data tag (<data>) and the end tag (</data>) and never outside of the tag or else you will encounter an error. At this time the only "Scene" tags are the only readable tags as the next tag after "Data" tags.

### Example
```
<Data>
	<-- Scene tags go here -->
</Data>
```

## Scene Tag
This is where the magic happens and the RPG comes alive with a story. I like to think that a scene is like one of the old 2D screen base adventures (Kings Quest) and it may have many interactive parts to the scene or "Screen". A scene tag has an attribute called "title", which is the title of the scene. This is important to fill out since it used by the computer to locate and failure to name a scene would result in a unreachable scene. Since "title" is used to locate scene you MUST keep the titles unique and in all lower-case, space-less, and free of symbols (@, #, %, *, ect...) to keep everything neat and to prevent any strange behaviours in future builds. 

There is a special scene title you must use for every game and that's the "start" title. This is the first scene that is presented to the player after character creation. As in the rules above there can only be ONE "start" title, at least at this time.

### Example
```
<data>
	<scene title="start">
		<-- Description, buttons, items, conditions tags go here -->
	</scene>
	
	<scene title="forest1">
		<-- Description, buttons, items, conditions tags go here -->
	</scene>
</data>
```

## Dialogue Tag
Every scene will need a dialogue tag for how you are going to present the scene. You do not necessary need text in the dialogue tag but it's needed for the use of "Tokens" and "Triggers" for the game. The use of "Tokens" are to make dynamic changes in parts of the text that contain these tokens. The "Triggers" can be alternative text based on an action or tokens you wish to display after a condition is meet.

The simplest token to use is the "@items" token, which will replace the token with a list of items from the item tags. If you wish to break the list into smaller pieces you can by using "@item#", where you replace the pound sign with a number cosponsoring to the position of the item tag starting at zero. A tag that can be called a semi-trigger is "@condition#", where it will display a "Failed" or "Success" tagged message from the cosponsoring condition also starting at zero.

### Example
```
<data>
	<scene title="start">
		<dialogue>This the start scene.</dialogue>
		<-- Buttons, items, conditions tags go here -->
	</scene>
	
	<scene title="fruititems">
		<dialogue>This is the list of items: @items. This is the first item of the list: @item0</dialogue>
		<item key="apple"\>
		<item key="pear"\>
		<item key="orange"\>
		<-- Buttons and conditions tags go here -->
	</scene>
	
	<scene title="testcondition">
		<dialogue>@condition0</dialogue>
		<condition type="hasItem" itemType="test" met="false">
			<fail>If you don't have the "Test" item you will see this</fail>
			<success>Otherwise you will see this message if you have the "Test" item</success>
		</condition>
		<-- Button tags go here -->
	</scene>
</data>
```

## Item Tag
Some times you have items placed in your scene that you would like the play to pick up and place in his inventory for use. Item tags allow you to place said items in the scene and become dynamic as they are removed from the scene or placed in the scene with clever use of the "@item" tags in the scene dialogue. There is only ONE attribute at the moment for item tag and it's the key word that will link to the items imported from storyitems.xml, stored in a hash-table (Think dictionary book specificity for the game) for retrieval. Please see the story item section below for more in-depth look at the structure and properties of items used in the game.

### Example
```
<data>
	<scene title="start">
		<dialogue>This is the list of fruit: @items. This is the first fruit of the list: @item0</dialogue>
		<item key="apple"\>
		<item key="pear"\>
		<item key="orange"\>
		<-- Buttons and conditions tags go here -->
	</scene>
</data>
```

## Condition Tag
The heavy lifting of the game which can alter and change responses based on conditions met. Sadly these can become quite complex and will require some testing and tweaking to be what you want the condition tag to do. You can have condition tags added to the scene or added to buttons which in turn can show or hide them if the conditions are met or not. A condition MUST have a "type" and have a default "met" state and depending on the condition type extra parameters to support the condition. Since conditions can have a success if met or a failure if not met you will have to add the <fail> and <success> tag in-between the <condition> tags. Currently all you need to do with the success and fail tag is keep it empty of text or add text if you are using the "@condition" token.

If the condition is added to a button, the button will be hidden until the condition for it is met and then it will be displayed. You can also use the "@condition" tokens for the buttons if you would like to utilize the success and fail tag messages.

Please see the condition types section for a full list of the available conditions.

### Example
```
<data>
	<scene title="start">
		<dialogue>This the start scene and has @condition0</dialogue>
		<condition type="hasItem" itemType="test" met="false">
			<fail>a button for you to press not bound to this condition.</fail>
			<success>Otherwise you will see this message.</success>
		</condition>
		<button text="I am a button" titleKey="buttoncondition" action="" result="" visible="true">
	</scene>
	
	<scene title="buttoncondition">
		<dialogue>@condition0</dialogue>
		<button text="I am a button" titleKey="start" action="" result="" visible="true">
			<condition type="hasItem" itemType="test" met="false">
				<fail>If you don't have the "Test" item you will see this and no button</fail>
				<success>Otherwise you will see this message and button if you have the "Test" item</success>
			</condition>
		</button>
	</scene>
</data>
```

## Button Tag
Almost all scenes should have at least ONE button added to it, otherwise you can have your player get stuck at a scene and never progress. A "button" tag has a "text" attribute which is used to display the text on the button or what you want displayed on the button. The "titleKey" is an important attribute used to point or find the scene you wish to progress too. The "action" attribute is a bit complex in that it makes the button acts like a condition tag in that if it's successful you do what's in the "result" attribute. A button "visible" attribute should be set to true by default and be hidden by the condition if the button has one. Otherwise you can hide a button by setting it to false if you wish to hide the button during game play.

### Example
```
<data>
	<scene title="start">
		<dialogue>This the start scene</dialogue>
		<button text="I am a button" titleKey="button1" action="" result="" visible="true">
	</scene>
	
	<scene title="button1">
		<dialogue>Button has been pressed</dialogue>
		<button text="Use Red Key" titleKey="start" action="useItem:redkey" result="button2" visible="true">
		</button>
	</scene>
	
	<scene title="button2">
		<dialogue>You had a key to get here!</dialogue>
		<button text="Back to start" titleKey="start" action="" result="" visible="true">
		</button>
	</scene>
</data>
```

The types of button actions usable are:

**useItem**: Like the condition type it checks if the player has a the item in there innovatory for everything after the colon (:).

### Example
```
<button text="Use test" titleKey="start" action="useItem:test" result="actiontitle" visible="true">
```

## Condition Types
The types of conditions will dictate what "extra" information must be present in the condition tag. The types listed are as follows:

**hasItem**: Checks to see if a itemType specified is in the players inventory based on the item's key. The key used is the same one in the item tags.
### Example
```
<condition type="hasItem" **itemType**="itemKey" met="false">
```

**useItem**: ~*To be developed*~

**hasStatType**: ~*To be developed*~

**useEndurence**: ~*To be developed*~

# StoryItems XML API
Much more straight forward then the story XML in the sense that it follows set number of properties. These properties are what define what the item is and how it will relate to the conditions or game elements.

## Item Tag
Unlike the story item tags, these have a bit more properties attached to them. Currently there are 6 properties attached to an item. An item has a "key" which is the unique name for the item used in the story XML item tags and conditions. There is the "name" property of the item used in the item tokens for the dialogue as well as a "description" property for item examination. The "type" property tells what we can do with said item, currently a place holder for future development. The "equipment" property is used to place an item on one (or more) player slots for clothing or weapon. All items must have a "uses" property greater then 0 or else it starts off as "empty" or "broken" for future condition statements.

### Example
```
<item>
	<key>test</key>
	<name>Test Item</name>
	<description>A test item from StoryItem.xml</description>
	<type>Useable</type>
	<equipment>head</equipment>
	<uses>1</uses>
</item>
```

# StoryCombat XML API
~*To be developed*~