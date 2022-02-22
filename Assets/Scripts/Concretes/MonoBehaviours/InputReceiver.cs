using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using RTSGame.Concretes.MonoBehaviours;
using RTSGame.Enums;
using RTSGame.Events;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Recieves inputs and fires event in battle scene.
/// </summary>
public class InputReceiver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Fields

    private float _tapDuration;
    private bool _isHolding;
    private BattleUnit _currentHit;

    #endregion

    #region Interface

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsInputActive)
            return;

        var ray = Camera.main.ScreenPointToRay(eventData.position);
        var layerMask = LayerMask.GetMask(Constants.TAGS.BATTLE_UNIT_TAG);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

        if (hit)
        {
            _isHolding = true;
            _currentHit = hit.collider.GetComponent<BattleUnit>();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;

        // if player did not hold, then firing selection event.
        if (_tapDuration <= Constants.GAME_CONFIGS.HOLD_DURATION)
        {
            if (_currentHit != null)
            {
                if (_currentHit.Model.IsDead)
                    return;

                if (_currentHit.Model.UnitTeam != Team.Blue)
                    return;

                //MessageBroker.Default.Publish(new EventUnitCardTapped { UnitModel = _currentHit.Model });
                EventBus.EventUnitCardTapped?.Invoke(_currentHit.Model, null, true);
            }
        }

        _currentHit = null;
        _tapDuration = 0.0f;

        //MessageBroker.Default.Publish(new EventUnitCardReleased());
        EventBus.EventUnitCardReleased?.Invoke();
    }

    #endregion

    #region Mono Behaviour

    private void Update()
    {
        if (_isHolding)
        {
            _tapDuration += Time.deltaTime;
            if (_tapDuration >= Constants.GAME_CONFIGS.HOLD_DURATION)
            {
                _isHolding = false;
                var position = Camera.main.WorldToScreenPoint(_currentHit.transform.position);
                //MessageBroker.Default.Publish(new EventUnitCardTappedAndHold { Position = position, UnitModel = _currentHit.Model });
                EventBus.EventUnitCardTappedAndHold?.Invoke(_currentHit.Model, position);
            }
        }
    }

    #endregion
}
