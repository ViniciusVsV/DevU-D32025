using StateMachine;
using UnityEngine;

namespace Enemies.Grenadier
{
    public class Controller : BaseController
    {
        [Header("||===== States =====||")]
        [SerializeField] private int test;

        //Ao detectar o jogador, começa a segui-lo até uma distancia x

        //Pós chegar nessa distância, espera a próxima batida e começa a arremessar a granada

        //Após mais uma batida, arremessa a granada de fato

        //Entra em um estado de espera até o cooldown resetar

        //Repete
    }
}