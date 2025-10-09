using UnityEngine;

namespace StateMachine
{
    public abstract class BaseState : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Transform tr;
        protected Animator animator;
        protected SpriteRenderer spriteRenderer;
        protected BaseStateController controller;

        public void Setup
        (
            Rigidbody2D rb,
            Transform tr,
            Animator animator,
            SpriteRenderer spriteRenderer,
            BaseStateController controller
        )
        {
            this.rb = rb;
            this.tr = tr;
            this.animator = animator;
            this.spriteRenderer = spriteRenderer;
            this.controller = controller;
        }

        public virtual void StateEnter() { }
        public virtual void StateUpdate() { }
        public virtual void StateFixedUpdate() { }
        public virtual void StateExit() { }
    }
}