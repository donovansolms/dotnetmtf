
#ifndef TOUCHLIBOBJECT_H
#define TOUCHLIBOBJECT_H
#include "TouchlibWrapper.h"
#include "TouchScreenDevice.h"

#include "resource.h"

#ifdef UNMANAGED_EXPORTS
#define UNMANAGED_API __declspec(dllexport)
#else
#define UNMANAGED_API __declspec(dllimport)
#endif

using namespace touchlib;
/*
#include <cv.h>
#include <cxcore.h>
#include <highgui.h>

#include "TouchScreenDevice.h"

#include "resource.h"

using namespace touchlib;

#include <stdio.h>
#include <string>
*/
class UNMANAGED_API TouchObject : public ITouchListener
{
public:
	TouchObject();
	~TouchObject();
	void fingerDown(TouchData data);
	void fingerUpdate(TouchData data);
	void fingerUp(TouchData data);

//private:
	CTouchlibWrapper UNMANAGED_API wrapper;

};
#endif