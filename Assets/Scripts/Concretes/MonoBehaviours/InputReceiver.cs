using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.MonoBehaviours;
using RTSGame.Enums;
using RTSGame.Events;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReceiver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _tapDuration;
    private bool _isHolding;
    private BattleUnit _currentHit;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsInputActive)
            return;

        var ray = Camera.main.ScreenPointToRay(eventData.position);
        var layerMask = LayerMask.GetMask("BattleUnit");
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

        if (_tapDuration <= 2.0f)
        {
            if (_currentHit != null)
            {
                if (_currentHit.Model.IsDead)
                    return;

                if (_currentHit.Model.UnitTeam != Team.Blue)
                    return;

                MessageBroker.Default.Publish(new EventUnitCardTapped { UnitModel = _currentHit.Model });
            }
        }

        _currentHit = null;
        _tapDuration = 0.0f;
        MessageBroker.Default.Publish(new EventUnitCardReleased());
    }

    private void Update()
    {
        if (_isHolding)
        {
            _tapDuration += Time.deltaTime;
            if (_tapDuration >= 2.0f)
            {
                _isHolding = false;
                MessageBroker.Default.Publish(new EventUnitCardTappedAndHold
                {
                    Position = transform.position,
                    UnitModel = _currentHit.Model
                });
            }
        }
    }
}
