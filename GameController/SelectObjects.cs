using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class SelectObjects : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _textScores; // массив выделенных юнитов
	[SerializeField] private List<GameObject> _unit; // массив всех юнитов, которых мы можем выделить
	[SerializeField] private List<GameObject> _unitSelected; // массив всех юнитов, которых мы можем выделить
	public List<GameObject> UnitSelected => _unitSelected;
	public List<GameObject> Unit => _unit;
	public static List<GameObject> unit; // массив всех юнитов, которых мы можем выделить
	public static List<GameObject> unitSelected; // массив выделенных юнитов

	public GUISkin skin;
	private Rect rect;
	private bool draw;
	private Vector2 startPos;
	private Vector2 endPos;

	private void Awake()
	{
		unit = _unit;
		unitSelected = _unitSelected;
	}

	private void Update()
	{
		_textScores.text = "Сборщики             " + unitSelected.Count.ToString();
	}

	// проверка, добавлен объект или нет
	private bool CheckUnit(GameObject unit)
	{
		bool result = false;
		foreach (GameObject u in unitSelected)
		{
			Select();
			if (u == unit) result = true;
			Select();
		}
		Select();
		return result;
	}

	private void Select()
	{
		if (unitSelected.Count > 0)
		{
			for (int j = 0; j < unitSelected.Count; j++)
			{
				// делаем что-либо с выделенными объектами
				unitSelected[j].GetComponent<Spaceship>().SetSelected(true);
			}
		}
	}

	private void Deselect()
	{
		if (unitSelected.Count > 0)
		{
			for (int j = 0; j < unitSelected.Count; j++)
			{
				// отменяем то, что делали с объектоми
				unitSelected[j].GetComponent<Spaceship>().SetSelected(false);
			}
		}
	}

	private void OnGUI()
	{
		GUI.skin = skin;
		GUI.depth = 99;

		if (Input.GetMouseButtonDown(0))
		{
			startPos = Input.mousePosition;
			draw = true;
			Deselect();
		}

		if (Input.GetMouseButtonUp(0))
		{
			draw = false;
		}

		if (draw)
		{
			Deselect();
			unitSelected.Clear();
			endPos = Input.mousePosition;
			if (startPos == endPos) return;
			rect = new Rect(Mathf.Min(endPos.x, startPos.x),
							Screen.height - Mathf.Max(endPos.y, startPos.y),
							Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
							Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
							);

			GUI.Box(rect, "");

			for (int j = 0; j < unit.Count; j++)
			{
				// трансформируем позицию объекта из мирового пространства, в пространство экрана
				Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(unit[j].transform.position).x, Screen.height - Camera.main.WorldToScreenPoint(unit[j].transform.position).y);
				if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
				{
					if (unitSelected.Count == 0)
					{
						unitSelected.Add(unit[j]);
					}
					else if (!CheckUnit(unit[j]))
					{
						unitSelected.Add(unit[j]);
					}

					Select();
				}
			}
		}
	}
}