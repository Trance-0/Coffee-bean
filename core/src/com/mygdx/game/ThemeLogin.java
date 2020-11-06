package com.mygdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.scenes.scene2d.Stage;

public class ThemeLogin implements Screen {
    private Sandglass sandglass;
    private Stage stage;

    private Texture temp;
    private ActorSandglass actorSandglass;

    public ThemeLogin(Sandglass s) {
        // TODO Auto-generated constructor stub
        sandglass = s;
    }

    @Override
    public void show() {
        stage=new Stage();
        temp=new Texture(Gdx.files.internal("Sandglass.png"));


        actorSandglass=new ActorSandglass();

        stage.addActor(actorSandglass);
    }

    @Override
    public void render(float delta) {

stage.draw();
    }

    @Override
    public void resize(int width, int height) {

    }

    @Override
    public void pause() {

    }

    @Override
    public void resume() {

    }

    @Override
    public void hide() {

    }

    @Override
    public void dispose() {

    }
}
