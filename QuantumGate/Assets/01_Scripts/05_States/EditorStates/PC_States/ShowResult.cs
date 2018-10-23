using UnityEngine;

/// <summary>
/// Etat pour montrer le résultat du circuit quantique jusqu'a la ligne _row.
/// </summary>
namespace PC_States
{
    public class ShowResult : EditorState
{
    /// <summary>
    /// Numero de la ligne
    /// </summary>
    private int _row;
        //bwa
    /// <summary>
    /// Etat precedent
    /// </summary>
    private EditorState _previousState;

    /// <summary>
    /// </summary>
    /// <param name="context">Le SandBoxManager qui est le contexte du jeu</param>
    /// <param name="row">Numero de la ligne où l'evaluation du circuit s'arrete</param>
    /// <param name="previousState">Etat precedent</param>
    public ShowResult(Editor context, int row, EditorState previousState) : base(context)
    {
        _row = row;
        _previousState = previousState;
    }

    public override void OnEnter()
    {
        string res, Seq;
        int taille;
        Debug.Log("ShowResult");
        context.SetResultHeader("Result of row : " + _row);
        context.SetResultText(context.currentCircuit.Evaluate(_row).ToString());
        res = context.currentCircuit.Evaluate(_row).ToString();
        Seq = context.currentCircuit.Evaluate(_row).Sequence();


            // CAS NUMERO 1 1 POSSIBILITE

            GameObject ball1 = context.CreateBlackBall(368, 170, (float)0.3);
            GameObject ball2 = context.CreateBlackBall(400, 170, (float)0.3);
            GameObject ball3 = context.CreateBlackBall(432, 170, (float)0.3);




            //GameObject ball1 = context.CreateBlackBall(368, 170, (float)0.3);
            //GameObject ball2 = context.CreateBlackBall(400, 170, (float)0.3);
            //GameObject ball3 = context.CreateBlackBall(432, 170, (float)0.3);

            //GameObject ball4 = context.CreateBlackBall(368, 220, (float)0.3);
            //GameObject ball5 = context.CreateBlackBall(400, 220, (float)0.3);
            //GameObject ball6 = context.CreateBlackBall(432, 220, (float)0.3);


            taille = res.Length;

                context.SetresultPanelScale(new Vector3(1.0f, 1.0f, 1.0f));

 

            
            // message de test pour afficher la taille du résultat dans la console
            Debug.Log("taille ="+ taille);
            context.ShowResultPanel(true);

    }

    public override void OnExit() { context.ShowResultPanel(false); }

    public override void OnBackResultClick() { context.CurrentState = _previousState; }

    public override void OnBackButton() { OnBackResultClick(); }
    }
}
