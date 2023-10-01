
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
# Flags from
    ## https://www.countryflags.com/


TODO
- culture flags
- humanize on create kerbal

- skin color type and hair color weights
- add bald hair type
- presets for famous astronauts and people (Lux)
- add countries: canada, mexico
- bad colors (remove): Real/Russet, Burnt Umber, Caucasian/#ffad60
- good colors (keep): BeautifulTouch/AntiqueBrass, BeautifulTouch/DesertSand
- CulturePicture: 780px x 150px
- chatgpt provided biographies


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



/// LAYOUT

Portrait *  |   (description)  |   (edit)   |   (achievements)
Flag        |   Biography
First name  |
Surname     |
Nationality |

* star on portrait if IsVeteran

- icon for kerbal type?
- gender?




HOW TO GET CAREER LENGTH:
Utility.Kerbals[new KSP.Sim.impl.IGGuid(new System.Guid("0c36df19-f328-4779-861b-eb43621c5735"))].GetCareerLength(UT)



*/