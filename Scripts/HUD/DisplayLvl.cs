using DG.Tweening;
using Matcha.Dreadful;
using Matcha.Unity;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DisplayLvl : BaseBehaviour
{
	private int intToDisplay;
	private string legend = "Lvl: ";
	private Text textComponent;
	private CanvasScaler canvasScaler;
	private RectTransform rectTrans;
	private Sequence fadeIn;
	private Sequence fadeOutInstant;
	private Sequence fadeOutHUD;

	void Awake()
	{
		rectTrans = GetComponent<RectTransform>();
		Assert.IsNotNull(rectTrans);

		textComponent = GetComponent<Text>();
		Assert.IsNotNull(textComponent);
	}

	void Start()
	{
		canvasScaler = gameObject.GetComponentInParent<CanvasScaler>();
		Assert.IsNotNull(canvasScaler);

		canvasScaler.scaleFactor = CANVAS_SCALE;
		M.PositionInHUD(rectTrans, textComponent, LEVEL_ALIGNMENT, LEVEL_X_POS, LEVEL_Y_POS);

		// cache & pause tween sequences.
		(fadeOutInstant = MFX.Fade(textComponent, 0, 0, 0)).Pause();
		(fadeIn         = MFX.Fade(textComponent, 1, HUD_FADE_IN_AFTER, HUD_INITIAL_TIME_TO_FADE)).Pause();
		(fadeOutHUD     = MFX.Fade(textComponent, 0, HUD_FADE_OUT_AFTER, HUD_INITIAL_TIME_TO_FADE)).Pause();
	}

	void OnInitInteger(int initInt)
	{
		textComponent.text = legend + initInt;
		FadeInText();
	}

	void FadeInText()
	{
		fadeOutInstant.Restart();
		fadeIn.Restart();
	}

	void OnChangeInteger(int newInt)
	{
		textComponent.text = legend + newInt;
	}

	void OnFadeHud(bool status)
	{
		fadeOutHUD.Restart();
	}

	void OnEnable()
	{
		EventKit.Subscribe<int>("init lvl", OnInitInteger);
		EventKit.Subscribe<bool>("fade hud", OnFadeHud);
	}

	void OnDestroy()
	{
		EventKit.Unsubscribe<int>("init lvl", OnInitInteger);
		EventKit.Unsubscribe<bool>("fade hud", OnFadeHud);
	}
}
