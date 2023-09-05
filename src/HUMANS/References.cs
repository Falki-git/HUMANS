
/*

Source material

# Nationality and Culture
    ## https://github.com/KSP-RO/KerbalRenamer
        - Nationality per Culture weights
        - First and last names per Nation
        - TODO: copy licence

# Skin colors
    ## https://huebliss.com/skin-color-code/
        - Skin types: Real, Fair, Dark, Caucasian, Indian

    ## https://www.schemecolor.com/
        ### https://www.schemecolor.com/beautiful-touch.php
            - Skin type: BeautifulTouch

        ### https://www.schemecolor.com/ebony-skin.php
            - Skin type: Ebony

        ### https://www.schemecolor.com/normal-and-tan-skin-colors.php
            - Skin type: Natural

        ### https://www.schemecolor.com/asian-skin.php
            - Skin type: Asian

        ### https://www.schemecolor.com/black-guy-skin-colors.php
            - Skin type: Black

    ## original Kerbal skins - in-game assets


TODO
- skin color type and hair color weights
- add bald hair type
- nationality flags
- culture flags
- presets for fameous astronauts and people (Lux)


- KSC menu - "Kerbal Space Center (KSC)" - GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/Agency
    - Image-Text Holder -> image -> replace with Culture flag/logo
                        -> TextHolder -> location and space agency name?


KSC menu: GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/LaunchLocationFlyoutHeaderToggle


// THIS LOADS UP A NEW SPACE CENTER KSC LOGO
//using SpaceWarp.API.Assets;
//using UnityEngine;
//using UnityEngine.UI;
var newTexture = AssetManager.GetAsset<Texture2D>("com.github.falki.humans/images/usa_flag.png");
var gameobject = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/Agency/Image-Text Holder/Image");
newTexture.filterMode = FilterMode.Point;
var image = gameobject.GetComponent<UnityEngine.UI.Image>();
image.sprite = Sprite.Create(newTexture, new Rect(0, 0, 600, 400), new Vector2(0.5f, 0.5f));
{
    Material newMaterial = new Material(image.material);
    newMaterial.color = Color.white;
    image.material = newMaterial;
}


*/