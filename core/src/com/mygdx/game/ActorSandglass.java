package com.mygdx.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.scenes.scene2d.Actor;

public class ActorSandglass extends Actor {
    private Texture shape;
    private int height;
    private int width;
    private boolean rotating;
    private int degree;

    public ActorSandglass(){
        super();
        shape=new Texture(Gdx.files.internal("Sandglass.png"));
        height = 40;
        width = 42;
//        height = 1180;
//        width = 668;
    }

    public void act(float delta) {
        super.act(delta);
        if(rotating){
            this.setRotation(degree);
            degree++;
            if (degree%180==0){
                rotating=false;
            }
            if(degree==360){
                degree=0;
            }
        }
    }
    public void draw(Batch batch, float parentAlpha) {
        super.draw(batch, parentAlpha);
        batch.draw(shape, this.getX(), this.getY(), width, height);
//		batch.draw(animationRegion, this.getX(), this.getY(), 34, 24);
    }
    private void rotate(){
        rotating=true;
    }
}
