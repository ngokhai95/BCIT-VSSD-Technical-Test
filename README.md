# BCIT-VSSD-Technical-Test

Demo here: https://play.unity.com/mg/other/bcit-vssd-technical-test-demo

# Project Description:

The project contains 5 main scripts, a ultility class, 4 materials for different model effects and a model prefab.

 - A ultility helper class contains a function to loop through and get all the descedants of an inputed model into a list.
 - A Spawner script that take a model prefab and spawn it in the middle of the screen with an offset.
 - A UserInteraction script that handles user mouse interaction such as hovering and clicking.
 - A ModelInteraction script that handles model movement, rotation, and single part movement.
 - A TreeListSetup script that populates the dropdown with the model descensdants names while keeping its hierarchy.
 - A ButtonHandler script that contains the functions for all the buttons.
 
The scene will only have UI elements like empty dropdown, buttons pre-defined. The rest will get populated at runtime.
