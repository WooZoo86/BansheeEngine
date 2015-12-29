#include "BsScriptSceneGrid.h"
#include "BsScriptCamera.h"
#include "BsEditorApplication.h"
#include "BsSceneGrid.h"

namespace BansheeEngine
{
	ScriptSceneGrid::ScriptSceneGrid(MonoObject* object, const SPtr<Camera>& camera)
		:ScriptObject(object), mSceneGrid(nullptr)
	{
		mSceneGrid = bs_new<SceneGrid>(camera);
		mSceneGrid->setSettings(gEditorApplication().getEditorSettings());
	}

	ScriptSceneGrid::~ScriptSceneGrid()
	{
		bs_delete(mSceneGrid);
	}

	void ScriptSceneGrid::initRuntimeData()
	{
		metaData.scriptClass->addInternalCall("Internal_Create", &ScriptSceneGrid::internal_Create);
		metaData.scriptClass->addInternalCall("Internal_Draw", &ScriptSceneGrid::internal_Draw);
	}

	void ScriptSceneGrid::internal_Create(MonoObject* managedInstance, ScriptCamera* camera)
	{
		new (bs_alloc<ScriptSceneGrid>()) ScriptSceneGrid(managedInstance, camera->getInternal());
	}

	void ScriptSceneGrid::internal_Draw(ScriptSceneGrid* thisPtr)
	{
		thisPtr->mSceneGrid->update();
	}
}