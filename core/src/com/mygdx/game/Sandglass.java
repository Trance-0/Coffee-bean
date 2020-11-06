package com.mygdx.game;

import com.badlogic.gdx.Game;
import com.badlogic.gdx.Preferences;

public class Sandglass extends Game {
	private Preferences prefs;

	private ThemeLoading a;
	private ThemeMain b;
	private ThemeSetting c;
	private ThemeLogin d;

	public void setPreference(Preferences p) {
		prefs = p;
	}

	public Preferences getPreference() {
		return prefs;
	}

	public void clearAll() {
		if (a != null) {
			a.dispose();
		}
		if (b != null) {
			b.dispose();
		}
		if (c != null) {
			c.dispose();
		}
		if (d != null) {
			d.dispose();
		}
	}

	public void showMain() {
		// TODO Auto-generated method stub
		clearAll();
		b = new ThemeMain(this);
		this.setScreen(b);
	}

	@Override
	public void create() {
		// TODO Auto-generated method stub

	}

	public void showLogin() {
		clearAll();
		d = new ThemeLogin(this);
		this.setScreen(d);
	}

}